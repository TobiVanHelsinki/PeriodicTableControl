using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobiVanHelsiki.PeriodicTableControl.Model
{
    public class Element : INotifyPropertyChanged
	{
        #region NotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        string _Name;
        public string Name
        {
            get { return _Name; }
            set { if (_Name != value) { _Name = value; NotifyPropertyChanged(); } }
        }
        int _Number;
        public int Number
        {
            get { return _Number; }
            set { if (_Number != value) { _Number = value; NotifyPropertyChanged(); } }
        }
        string _Symbol;
        public string Symbol
        {
            get { return _Symbol; }
            set { if (_Symbol != value) { _Symbol = value; NotifyPropertyChanged(); } }
        }
        string _ElectronConfig;
        public string ElectronConfig
        {
            get { return _ElectronConfig; }
            set { if (_ElectronConfig != value) { _ElectronConfig = value; NotifyPropertyChanged(); } }
        }
        int _Period;
        public int Period
        {
            get { return _Period; }
            set { if (_Period != value) { _Period = value; NotifyPropertyChanged(); } }
        }
        int _Group;
        public int Group
        {
            get { return _Group; }
            set { if (_Group != value) { _Group = value; NotifyPropertyChanged(); } }
        }

        public ElementTypes Type { get; internal set; }
        public ElementPhases Phase { get; internal set; }
        public int AtomicWeight { get; internal set; }
        public enum ElementPhases
        {
            Solid,
            Liquid,
            Gas,
            Artificial,
            Unknown
        }

        public enum ElementTypes
        {
            Actinide,
            AlkaliMetal,
            AlkalineEarthMetal,
            Halogen,
            Lanthanide,
            Metal,
            Metalloid,
            NobleGas,
            Nonmetal,
            Transactinide,
            TransitionMetal,
            Unknown
        }
    }
}
