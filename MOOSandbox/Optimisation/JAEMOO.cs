using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOOSandbox.Optimisation
{
	public abstract class JAEMOOE
	{
		/*
		 *	Constructors
		 */
		protected JAEMOOE() { }
		/*
		 *  Static Members
		 */

		/*
		 *  Static Methods
		 */
		/*
		 *  Instance Members
		 */
		int Popluation { get; set; }		= 10;
		int Generations { get; set; }		= 10;
		double CrossoverRate { get; set; }	= 0.9;
		double MutationRate { get; set; }	= 0.1;
		/*
		 *  Instance Methods
		 */

	}
}
