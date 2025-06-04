using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single; 

namespace MOOSandbox.DataManagement
{
	public class MathNetRetrofitsTable
	{
		/*
		 *	Constructors
		 */
		public MathNetRetrofitsTable(CsvHandler csv, string[] retrofitAliases)
		{
			CsvHandler			= csv;
			ActiveBuildingMask	= Enumerable.Repeat(1, csv.Length).ToArray();
			BuildTables(retrofitAliases);
			
		}

		/*
		 *  Static Members
		 */
		public static readonly string DIFF_SUFFIX	= "-Diff";
		public static readonly string COST_SUFFIX	= "-Diff";
		/*
		 *  Static Methods
		 */
		/*
		 *  Instance Members
		 */
		protected CsvHandler CsvHandler { get; set; }
		protected bool BuiltTables { get; set; }
		protected int[] ActiveBuildingMask;
		protected int[][] Retrofits				=  Array.Empty<int[]>();
		protected Matrix<float> Costs			= Matrix<float>.Build.Dense(0, 0);
		protected Matrix<float> Differences		= Matrix<float>.Build.Dense(0, 0);
		protected Dictionary<string, Vector<float>> DataTables { get; set; } = new Dictionary<string, Vector<float>>();
		/*
		 *  Instance Methods
		 */
		public void BuildTables(string[] retrofitAliases)
		{
			// Make sure we don't build the tables again
			if (BuiltTables) { return; }
			float[][] costs = new float[retrofitAliases.Length][];
			float[][] diffs = new float[retrofitAliases.Length][];
			// Prepare all retroifts
			for (int aliasID = 0; aliasID < retrofitAliases.Length; aliasID++)
			{
				string costColumnName = retrofitAliases[aliasID] + COST_SUFFIX;
				string diffColumnName = retrofitAliases[aliasID] + DIFF_SUFFIX;
				costs[aliasID] = CsvHandler.GetNumericColumnValues(costColumnName);
				diffs[aliasID] = CsvHandler.GetNumericColumnValues(diffColumnName);
			}
			// Transpose the table
			int rows = CsvHandler.Length;
			int cols = retrofitAliases.Length;
			float[,] transposedCosts = new float[cols, rows];
			float[,] transposedDiffs = new float[cols, rows];

			for (int columnID = 0; columnID < cols; columnID++)
			{
				for (int rowID = 0; rowID < rows; rowID++)
				{
					transposedCosts[rowID, columnID] = costs[columnID][rowID];
					transposedDiffs[rowID, columnID] = diffs[columnID][rowID];
				}
			}
			// Create arrays
			Costs		= Matrix.Build.DenseOfArray(transposedCosts);
			Differences	= Matrix.Build.DenseOfArray(transposedDiffs);
			// Make sure we don't build the tables again
			BuiltTables = true;
		}
		/*
			Create dictionary
		 */
		public bool CreateDictionary(string columnName)
		{
			// Return false if the key doesn't exist in the Csv data
			if(!CsvHandler.ColumnExists(columnName)) 
				return false;
			// Return false if the dictionary already exists
			if(DataTables.ContainsKey(columnName)) 
				return false; 
			if(!CsvHandler.ColumnIsNumeric(columnName)) 
				return false;
			// Add the new 
			DataTables[columnName] = Vector.Build.DenseOfArray(CsvHandler.GetNumericColumnValues(columnName).ToArray());
			return true;
		}
	}
}
