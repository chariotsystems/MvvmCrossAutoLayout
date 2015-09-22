using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmCrossAutoLayout.Interfaces
{
	public interface IMvxTableRowCommands
	{
		ICommand RemoveCommand { get; }

		ICommand SelectedCommand { get; }
	}
}
