using System.Collections.Generic;
using CAS.UI.UIControls.OpepatorsControls;

namespace CAS.UI.UIControls.Auxiliary.Comparers
{
    /// <summary>
    /// ��������� <see cref="ReferenceOperatorListItem"/>
    /// </summary>
    public class OperatorListItemComparer : IComparer<ReferenceOperatorListItem>
    {
        #region IComparer<OperatorListItem> Members
        ///<summary>
        ///Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        ///</summary>
        ///<returns>
        ///Value Condition Less than zerox is less than y.Zerox equals y.Greater than zerox is greater than y.
        ///</returns>
        ///<param name="y">The second object to compare.</param>
        ///<param name="x">The first object to compare.</param>
        public int Compare(ReferenceOperatorListItem x, ReferenceOperatorListItem y)
        {
            return string.Compare(x.Text, y.Text);
        }
        #endregion
    }
}