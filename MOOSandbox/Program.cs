using GeneticSharp;
using MOOSandbox.Chromosome;
using MOOSandbox.DataManagement;
using MOOSandbox.Building;
using System;

/*
 *  Math.NET  
 */
MathNet.Numerics.Control.UseNativeMKL();


string EXAMPLE_CSV_PATH = "c:/workspaces/__sandbox__/moo_sharp_sandbox/MOOSandbox/examples/stockton/data.csv";
int N_VARIABLES = 100;

// Load data
CsvHandler csvHandler		= CsvHandler.ParseCSV(EXAMPLE_CSV_PATH);
csvHandler.PrintErrors();

MathNetRetrofitsTable data = new MathNetRetrofitsTable(csvHandler, RetrofitOption.ALL_RETROFIT_OPTION_KEYS.ToArray() );
Console.WriteLine(data.SumCosts(new int[] { 1, 2 }, new int[] { 1, 2 }));
return;
// Define the chromosome: four genes representing x1, y1, x2, y2
//var chromosome = new MixedIntegerChromosome(
//	new int[N_VARIABLES],       // Lower bounds
//	Enumerable.Repeat<int>(100, N_VARIABLES).ToArray()  // All values = 42// Upper bounds
//);

//// Define the fitness function: maximize Euclidean distance
//var fitness = new FuncFitness(c =>
//{
//	MixedIntegerChromosome fc	= (MixedIntegerChromosome)c;
//	int[] values				= fc.GetValues();

//	//for (int i = 0; i < N_VARIABLES; i++)
//	//	for (int j = 0; j < N_VARIABLES; j++)
//	//		score += 1; // TODO: This is where the data
//	//return data.Sum()
//	return 0.1;
//});

//// Create the population
//var population = new Population(50, 500, chromosome);

//// Configure the genetic algorithm
//var ga = new GeneticAlgorithm(
//	population,
//	fitness,
//	new EliteSelection(),
//	new UniformCrossover(),
//	new UniformMutation(true)
//);

//ga.Termination = new FitnessStagnationTermination(50);

//// Subscribe to the GenerationRan event to output progress
//ga.GenerationRan += (sender, e) =>
//{
//	var bestChromosome = ga.BestChromosome as MixedIntegerChromosome;
//	var bestValues = bestChromosome.GetValues();
//	var bestFitness = bestChromosome.Fitness.Value;
//	Console.WriteLine($"Generation {ga.GenerationsNumber}: Best Fitness = {bestFitness:F2}");
//	Console.WriteLine($"Points: ({bestValues[0]:F2}, {bestValues[1]:F2}) to ({bestValues[2]:F2}, {bestValues[3]:F2})");
//};

//// Start the genetic algorithm
//ga.Start();
