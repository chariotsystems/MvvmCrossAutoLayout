using System;

namespace MvvmCrossAutoLayout.Services
{
	public interface IKittenGenesisService
	{
		Kitten CreateNewKitten (string extra = "");
	}
}

