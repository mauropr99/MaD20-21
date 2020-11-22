using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.LabelDao;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentService: ICommentService
    {
  
        [Inject]
        public ICommentDao CommentDao { private get; set; }
        [Inject]
        public ILabelDao LabelDao { private get; set; }

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
                    comment.Labels.Add(foundLabel);
                }
            }

            CommentDao.Create(comment);
            return comment;

        }

        public Comment UpdateComment(long commentId, string text, List<string> labels)
        {
            Comment comment = CommentDao.Find(commentId);

            //Recovering comment labels
            List<Label> oldLabels = LabelDao.FindByCommentId(commentId);
    
            foreach(string label in labels)
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
                    comment.Labels.Add(foundLabel);
                }

            }

            //Removing unused labels
            foreach (Label oldLabel in oldLabels)
            {
                if (!labels.Contains(oldLabel.lab))
                {
                    Label foundLabel = LabelDao.FindByLabelName(oldLabel.lab);
                    if (foundLabel.timesUsed == 1)
                    {
                        LabelDao.Remove(foundLabel.id);
                    } else
                    {
                        foundLabel.timesUsed--;
                        LabelDao.Update(foundLabel);
                    }
                    
                }
            }


            comment.text = text;

            CommentDao.Update(comment);

            return comment;
        }

        public void RemoveComment(long commentId)
        {
            //Check if comment exists
            CommentDao.Find(commentId);


            List<Label> labels = LabelDao.FindByCommentId(commentId);


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
