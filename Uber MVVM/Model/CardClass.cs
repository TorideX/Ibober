using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uber_MVVM.Model
{
    public class CardClass
    {
        public string Number { get; set; }
        public string ExpireDate { get; set; }
        public string CCV { get; set; }
        public string Owner { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as CardClass;
            return Number == o.Number && ExpireDate == o.ExpireDate &&
                CCV == o.CCV && Owner == o.Owner;
        }

        //public void Copy(CardClass card)
        //{
        //    Number = string.Copy(card.Number);
        //    ExpireDate = string.Copy(card.ExpireDate);
        //    CCV = string.Copy(card.CCV);
        //    Owner = string.Copy(card.Owner);
        //}
    }
}
