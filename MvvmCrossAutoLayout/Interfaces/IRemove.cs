using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmCrossAutoLayout.Interfaces
{
	public interface IRemove
	{
		ICommand RemoveCommand { get; }

		ICommand SelectedCommand { get; }
	}
}
