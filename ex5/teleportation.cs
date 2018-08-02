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
			Register x = comp.NewRegister(2, 2);
			Register y = comp.NewRegister(0, 1);
			x.SqrtX(1);
			x.Hadamard(0);
			comp.CNot(target: y[0], control: x[0]);
			x.CNot(target: 0, control: 1);
			x.Hadamard(1);
			x.Measure(0);
			x.Measure(1);
			comp.CNot(target: y[0], control: x[0]);
			comp.SigmaZ(y[0], x[1]);
		}
	}
}
