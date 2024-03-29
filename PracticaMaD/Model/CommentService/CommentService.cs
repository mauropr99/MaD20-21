﻿using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.LabelDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentService : ICommentService
    {
        [Inject]
        public ICommentDao CommentDao { private get; set; }
        [Inject]
        public ILabelDao LabelDao { private get; set; }

        [Inject]
        public IUserDao UserDao { private get; set; }

        #region ICommentSercice Members
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="ProductAlreadyCommentedException"/>
        public Comment NewComment(long userId, long productId, string text, List<string> labels)
        {
            User user = UserDao.Find(userId);
            foreach (Comment foundComment in user.Comments)
            {
                if (foundComment.productId == productId)
                    throw new ProductAlreadyCommentedException(productId);
            }

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
                foreach (string label in labels)
                {

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


        /// <exception cref="InstanceNotFoundException"/>
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
                        CommentDao.RemoveLabel(foundLabel, commentId);
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
                        timesUsed = 0
                    };
                    LabelDao.Create(newLabel);
                }

                Label foundLabel = LabelDao.FindByLabelName(label);
                if (!oldLabels.Contains(foundLabel)) foundLabel.timesUsed++;
                LabelDao.Update(foundLabel);

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

        /// <exception cref="InstanceNotFoundException"/>
        public void RemoveComment(long userId, long commentId)
        {
            //Check if comment exists
            Comment comment = CommentDao.Find(commentId);

            if (comment.userId != userId) throw new DifferentsUsers(userId);

            var labels = CommentDao.Find(commentId).Labels.ToList();


            //Removing unused labels
            foreach (Label label in labels)
            {
                if (label.timesUsed == 1)
                {
                    LabelDao.Remove(label.id);
                }
                else
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
            comments = CommentDao.FindCommentsByProductId(productId, startIndex, count + 1);


            bool existMoreComments = (comments.Count == count + 1);



            List<CommentDetails> detailComments = new List<CommentDetails>();


            foreach (Comment comment in comments)
            {
                User user = UserDao.Find(comment.userId);
                List<string> labelNames = new List<string>();
                foreach (Label label in comment.Labels)
                {
                    labelNames.Add(label.lab);
                }

                detailComments.Add(new CommentDetails(comment.id, user.login, user.id, comment.commentDate, comment.text, labelNames));
            }

            if (existMoreComments)
            {
                detailComments.RemoveAt(count);
            }
            return new CommentBlock(detailComments, existMoreComments);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public CommentDetails FindComment(long commentId)
        {
            Comment comment = CommentDao.Find(commentId);

            User user = UserDao.Find(comment.userId);
            List<string> labelNames = new List<string>();
            foreach (Label label in comment.Labels)
            {
                labelNames.Add(label.lab);
            }

            CommentDetails commentDetails = new CommentDetails(commentId, user.login, user.id, comment.commentDate, comment.text, labelNames);

            return commentDetails;
        }

        /// <exception cref="InstanceNotFoundException"></exception>
        public List<LabelDetails> ViewMostUsedLabels(int quantity)
        {
            List<LabelDetails> mostUsedLabels = new List<LabelDetails>();

            List<Label> labels = LabelDao.FindMostUsedLabel(quantity);

            foreach (Label label in labels)
            {
                int aux = 0;
                if (label.timesUsed != null) aux = (int)label.timesUsed;

                mostUsedLabels.Add(new LabelDetails(label.id, label.lab, aux));
            }

            return mostUsedLabels;
        }

        /// <exception cref="InstanceNotFoundException"></exception>
        public bool UserAlreadyCommented(long productId, long userId)
        {
      
            try {
                CommentDao.FindCommentsByUserId(productId, userId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion ICommentSercice Members
    }
}
