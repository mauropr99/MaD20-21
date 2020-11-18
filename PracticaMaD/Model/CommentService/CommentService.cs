using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.LabelDao;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentService: ICommentService
    {
        public CommentService()
        {
        }

        [Inject]
        public ICommentDao CommentDao { private get; set; }
        [Inject]
        public ILabelDao LabelDao { private get; set; }

        #region ICommentSercice Members

        public Comment NewComment(long userId, long productId, string text, List<Label> labels)
        {
            Comment comment = new Comment();
            
            if (labels.Count != 0)
            {
                foreach (Label label in labels) {
                    
                    if (!LabelDao.ExistByName(label.lab)) 
                    {
                        LabelDao.Create(label);
                    }

                    comment.Labels.Add(label);
                }
            }

            comment.userId = userId;
            comment.productId = productId;
            comment.text = text;

            CommentDao.Create(comment);

            return comment;

        }

        public Comment UpdateComment(long commentId, string text, List<Label> labels)
        {
            Comment comment = CommentDao.Find(commentId);

            List<Label> labelsComment = LabelDao.FindByCommentId(commentId);
    
            foreach(Label label in labelsComment)
            {
                if (!labels.Contains(label))
                {
                    comment.Labels.Remove(label);
                }
            }


            comment.text = text;

            CommentDao.Update(comment);

            return comment;
        }

        public Comment RemoveComment(long commentId)
        {
            throw new NotImplementedException();
        }

        public CommentBlock ViewComments(long productId, int startIndex, int count)
        {
            List<Comment> comments;

            /*
            * Find count+1 orders to determine if there exist more orders above
            * the specified range.
            */
            comments = CommentDao.FindByProductId(productId, startIndex, count + 1);

            bool existMoreComments = (comments.Count == count + 1);

            if (existMoreComments)
            {
                comments.RemoveAt(count);
            }

            List<CommentDetails> detailComments = new List<CommentDetails>();
            List<string> labelNames = new List<string>();

            foreach (Comment comment in comments)
            {
                foreach(Label label in comment.Labels)
                {
                    labelNames.Add(label.lab);
                }

                detailComments.Add(new CommentDetails(comment.id, comment.User_Table.login, comment.commentDate, comment.text, labelNames));
            }

            return new CommentBlock(detailComments, existMoreComments);
        }

        #endregion ICommentSercice Members
    }
}
