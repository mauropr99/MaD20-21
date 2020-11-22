using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentDetails
    {
        #region Properties Region

        public long Id { get; private set; }

        public string Login { get; private set; }

        public DateTime Date { get; private set; }

        public string Text { get; private set; }

        public List<string> Labels { get; private set; }

        #endregion Properties Region

        public CommentDetails(long id, string login, DateTime date, string text, List<string> labels)
        {
            Id = id;
            Login = login;
            Date = date;
            Text = text;
            Labels = labels;
        }

        public override bool Equals(object obj)
        {
            return obj is CommentDetails details &&
                   Id == details.Id &&
                   Login == details.Login &&
                   Date == details.Date &&
                   Text == details.Text &&
                   EqualityComparer<List<string>>.Default.Equals(Labels, details.Labels);
        }

        public override int GetHashCode()
        {
            int hashCode = 1930600707;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Login);
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(Labels);
            return hashCode;
        }

        public override string ToString()
        {
            string strCommentDetails;

            strCommentDetails =
                "[ login = " + Login + "| " +
                " date = " + Date + " | " +
                " text = " + Text + " | " +
                " label = " + Labels.ToString() + " ] ";

            return strCommentDetails;
        }
    }

}
