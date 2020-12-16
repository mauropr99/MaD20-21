using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.LabelDao;
using Ninject;
using System.Linq;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentService: ICommentService
    {
 

        [Inject]
        public ICommentDao CommentDao { private get; set; }
        [Inject]
        public ILabelDao LabelDao { private get; set; }

        [Inject]
        public IUserDao UserDao { private get; set; }

        #region ICommentSercice Members

        public Comment NewComment(long userId, long productId, string text, List<string> labels)
        {
            Comment comment = new Comment
            {
                userId = userId,
                productId = productId,
                text = text,
                commentDate = DateTime.Now
            };

            CommentDao.Create(comment);

            if (labels.Count != 0)
            {
                foreach (string label in labels) {

                    if (!LabelDao.ExistByName(label)) 
                    {
                        Label newLabel = new Label()
                        {
                            lab = label,
                            timesUsed = 0
                        };
                        LabelDao.Create(newLabel);
                    }

                    Label foundLabel = LabelDao.FindByLabelName(label);

                    foundLabel.timesUsed++;
                    LabelDao.Update(foundLabel);
                    CommentDao.AddLabel(foundLabel, comment.id);
                }
            }

            CommentDao.Update(comment);
            return comment;

        }

        public Comment UpdateComment(long userId, long commentId, string text, List<string> labels)
        {
            Comment comment = CommentDao.Find(commentId);

            if (comment.userId != userId) throw new DifferentsUsers(userId);

            //Recovering comment labels
            List<Label> oldLabels = LabelDao.FindLabelsByCommentId(commentId);



            //Removing unused labels
            foreach (Label oldLabel in oldLabels)
            {
                if (!labels.Contains(oldLabel.lab))
                {
                    Label foundLabel = LabelDao.FindByLabelName(oldLabel.lab);
                    if (foundLabel.timesUsed == 1)
                    {
                        LabelDao.Remove(foundLabel.id);
                    }
                    else
                    {
                        foundLabel.timesUsed--;
                        LabelDao.Update(foundLabel);
                    }

                }
            }

            foreach (string label in labels)
            {

                if (!LabelDao.ExistByName(label))
                {
                    Label newLabel = new Label()
                    {
                        lab = label,
                        timesUsed = 1
                    };
                    LabelDao.Create(newLabel);
                }

                Label foundLabel = LabelDao.FindByLabelName(label);

                //Adding new labels
                if (!oldLabels.Contains(foundLabel))
                {
                    CommentDao.AddLabel(foundLabel, comment.id);
                }

            }


            comment.text = text;
            CommentDao.Update(comment);
            return comment;
        }

        public void RemoveComment(long userId, long commentId)
        {
            //Check if comment exists
            Comment comment =  CommentDao.Find(commentId);

            if (comment.userId != userId) throw new DifferentsUsers(userId);

            var labels = CommentDao.Find(commentId).Labels.ToList();


            //Removing unused labels
            foreach (Label label in labels)
            {
                if (label.timesUsed == 1)
                {
                    LabelDao.Remove(label.id);
                } else
                {
                    label.timesUsed--;
                    LabelDao.Update(label);
                }
            }

            CommentDao.Remove(commentId);
        }

        public CommentBlock ViewComments(long userId, long productId, int startIndex, int count)
        {
            List<Comment> comments;

            /*
            * Find count+1 orders to determine if there exist more orders above
            * the specified range.
            */
            comments = CommentDao.FindByProductId(productId, startIndex, count + 1);
            User user = UserDao.Find(userId);

            bool existMoreComments = (comments.Count == count + 1);

            if (existMoreComments)
            {
                comments.RemoveAt(count);
            }

            List<CommentDetails> detailComments = new List<CommentDetails>();

            
            foreach (Comment comment in comments)
            {
                List<string> labelNames = new List<string>();
                foreach (Label label in comment.Labels)
                {
                    
                    labelNames.Add(label.lab);
                }

                detailComments.Add(new CommentDetails(comment.id, user.login, comment.commentDate, comment.text, labelNames));
            }

            return new CommentBlock(detailComments, existMoreComments);
        }

        #endregion ICommentSercice Members
    }
}
