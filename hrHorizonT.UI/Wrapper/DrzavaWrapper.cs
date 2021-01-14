using hrHorizonT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace hrHorizonT.UI.Wrapper
{
    public class DrzavaWrapper : ModelWrapper<Drzava>
    {
        public DrzavaWrapper(Drzava model) : base(model)
        {
        }
        public int Id { get { return Model.Id; } }

        public int? Sifra
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public string Oznaka
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Naziv
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        
    }
}
