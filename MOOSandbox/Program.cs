using GeneticSharp;
using MOOSandbox.Chromosome;
using System;

int N_VARIABLES = 100;
// Define the chromosome: four genes representing x1, y1, x2, y2
var chromosome = new MixedIntegerChromosome(
	new int[N_VARIABLES],       // Lower bounds
	Enumerable.Repeat<int>(100, N_VARIABLES).ToArray()  // All values = 42// Upper bounds
);

// Define the fitness function: maximize Euclidean distance
var fitness = new FuncFitness(c =>
{
	MixedIntegerChromosome fc = (MixedIntegerChromosome)c;
	var values = fc.GetValues();
	double dx = values[2] - values[0];
	double dy = values[3] - values[1];
	return Math.Sqrt(dx * dx + dy * dy);
});

// Create the population
var population = new Population(50, 500, chromosome);

// Configure the genetic algorithm
var ga = new GeneticAlgorithm(
	population,
	fitness,
	new EliteSelection(),
	new UniformCrossover(),
	new UniformMutation(true)
);

ga.Termination = new FitnessStagnationTermination(50);

// Subscribe to the GenerationRan event to output progress
ga.GenerationRan += (sender, e) =>
{
	var bestChromosome = ga.BestChromosome as MixedIntegerChromosome;
	var bestValues = bestChromosome.GetValues();
	var bestFitness = bestChromosome.Fitness.Value;
	Console.WriteLine($"Generation {ga.GenerationsNumber}: Best Fitness = {bestFitness:F2}");
	Console.WriteLine($"Points: ({bestValues[0]:F2}, {bestValues[1]:F2}) to ({bestValues[2]:F2}, {bestValues[3]:F2})");
};

// Start the genetic algorithm
ga.Start();
