using Quantum;
using Quantum.Operations;
using System;
using System.Numerics;
using System.Collections.Generic;

namespace QuantumConsole
{
	public class QuantumTest
	{
		public static void Main()
		{
			QuantumComputer comp = QuantumComputer.GetInstance();
			Register x = comp.NewRegister(0, 3);
			x.Toffoli(1, 2, 0);		// Toffoli(<target_bit>, ... <control_bits> ...)
		}
	}
}
