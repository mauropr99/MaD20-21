using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class LabelDetails
    {
        #region Properties Region

        public long Id { get; private set; }

        public string LabelName  { get; private set; }

        #endregion Properties Region

        public LabelDetails(long id, string labelName)
        {
            Id = id;
            LabelName = labelName;
        }

            

        public override string ToString()
        {
            string strCommentDetails;

            strCommentDetails =
                "[ labelName = " + LabelName + " ] ";

            return strCommentDetails;
        }

        public override bool Equals(object obj)
        {
            return obj is LabelDetails details &&
            Id == details.Id &&
            LabelName == details.LabelName;
        }

        public override int GetHashCode()
        {
            var hashCode = 736954558;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LabelName);
            return hashCode;
        }
    }

}