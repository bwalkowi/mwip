using Quantum;
using Quantum.Operations;
using System;
using System.Numerics;
using System.Collections.Generic;

namespace QuantumConsole
{
	public class QuantumTest
	{
		static QuantumComputer comp = QuantumComputer.GetInstance();

		public static void Main()
		{
			Register x = comp.NewRegister(1, 1);
			Register y = comp.NewRegister(1, 1);

			x.Hadamard(0);
			y.Hadamard(0);
			f1(x, y);
			x.Hadamard(0);
			y.Hadamard(0);
		}

		public static void f0(Register x, Register y){
			return;
		}
		
		public static void f1(Register x, Register y){
			comp.CNot(control: x[0], target: y[0]);
			return;
		}

		public static void f2(Register x, Register y){
			y.SigmaX(0);
			comp.CNot(control: x[0], target: y[0]);
			return;
		}

		public static void f3(Register x, Register y){			
			y.SigmaX(0);
			return;
		}
	}
}
