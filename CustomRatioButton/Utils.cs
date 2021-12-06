using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomRatioButton
{
    class Utils
    {

        // whatever
    }


    public class Option
    {
        public String Name { get; set; }
       
        public int Number { get; set; }

        public override string ToString() => Name;
    }


    public abstract class AbstractRelayCommand : ICommand
    {
        public Func<object, bool> ExectutionChecker { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (ExectutionChecker == null)
                return true;

            return ExectutionChecker(parameter);
        }

        public abstract void Execute(object parameter);
    }


    public class RelayCommand : AbstractRelayCommand
    {

        public Action DefinedAction { get; }
        
        /// <summary>
        /// Initailisation of an Relay command 
        /// </summary>
        /// <param name="func"></param>
        public RelayCommand(Action action)
        {
            DefinedAction = action;
        }

        public override void Execute(object parameter) => DefinedAction();
        
    }


    public class ParameterizedRelayCommand : AbstractRelayCommand
    {

        public Action<object> DefinedAction { get; }

        public ParameterizedRelayCommand(Action<object> action)
        {
            DefinedAction = action;
        }

        public override void Execute(object parameter) => DefinedAction(parameter);

    }
}
