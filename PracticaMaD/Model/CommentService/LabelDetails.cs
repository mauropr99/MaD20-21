using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class LabelDetails
    {
        #region Properties Region

        public long Id { get; private set; }

        public string LabelName { get; private set; }

        public int TimesUsed { get; private set; }

        #endregion Properties Region

        public LabelDetails(long id, string labelName, int timesUsed)
        {
            Id = id;
            LabelName = labelName;
            TimesUsed = timesUsed;
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
                   LabelName == details.LabelName &&
                   TimesUsed == details.TimesUsed;
        }

        public override int GetHashCode()
        {
            int hashCode = 1714619790;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LabelName);
            hashCode = hashCode * -1521134295 + TimesUsed.GetHashCode();
            return hashCode;
        }
    }

}