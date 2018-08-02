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
			Register x = comp.NewRegister(1, 3);
			x.Hadamard(0);
			x.Hadamard(1);
			x.Hadamard(2);

			a(x);
//			b(x);
//			c(x);
//			d(x);

			x.Hadamard(1);
			x.Hadamard(2);
			x.SigmaX(1);
			x.SigmaX(2);
			x.Hadamard(1);
			x.CNot(target: 1, control: 2);
			x.Hadamard(1);
			x.SigmaX(1);
			x.SigmaX(2);
			x.Hadamard(1);
			x.Hadamard(2);
		}
		
		
		public static void a(Register x){
			x.SigmaX(1);
			x.SigmaX(2);
			x.Toffoli(0, 1, 2);		// Toffoli(<target_bit>, ... <control_bits> ...)
			x.SigmaX(1);
			x.SigmaX(2);		
		}

		public static void b(Register x){
			x.SigmaX(1);
			x.Toffoli(0, 1, 2);		// Toffoli(<target_bit>, ... <control_bits> ...)
			x.SigmaX(1);
		}

		public static void c(Register x){
			x.SigmaX(2);
			x.Toffoli(0, 1, 2);		// Toffoli(<target_bit>, ... <control_bits> ...)
			x.SigmaX(2);		
		}

		public static void d(Register x){
			x.Toffoli(0, 1, 2);		// Toffoli(<target_bit>, ... <control_bits> ...)
		}
	}
}
