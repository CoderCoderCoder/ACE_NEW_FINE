using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackChromosome{

    private static int max_gene = 4;
    private int[] genes;
    private float fitness;
    private bool is_feasable;

    public TrackChromosome(int size){
        genes = new int[size];
        RandomInit();
    }

    private void RandomInit(){
        for (int i = 0; i < genes.Length; i++){
            genes[i] = RandomGene();
        }
    }

    public void SetFeasable(bool is_feasable){
        this.is_feasable = is_feasable;
    }

    public bool IsFeasable(){
        return is_feasable;
    }

    private static int RandomGene(){
        return Random.Range(0, max_gene + 1);
    }

    public void SetFitness(float fitness){
        this.fitness = fitness;
    }

    public float GetFitness(){
        return this.fitness;
    }

    public TrackChromosome(TrackChromosome c){
        genes = new int[c.genes.Length];
        for (int i = 0; i < c.genes.Length; i++)
        {
            genes[i] = c.genes[i];
        }
    }

    public void Mutate(){
        genes[Random.Range(0, genes.Length)] = RandomGene();         
    }

    public static List<TrackChromosome> Crossover(TrackChromosome c1, TrackChromosome c2){
        List<TrackChromosome> new_cromosomes = new List<TrackChromosome>();

        TrackChromosome new_c1 = new TrackChromosome(c1);
        TrackChromosome new_c2 = new TrackChromosome(c2);

        int x_point = Random.Range(0, c1.genes.Length);

        for (int i = 0; i < c1.genes.Length; i++){
            if(i<x_point){
                new_c1.genes[i] = c2.genes[i];
            }else{
                new_c2.genes[i] = c1.genes[i];
            }
        }

        new_cromosomes.Add(new_c1);
        new_cromosomes.Add(new_c2);

        return new_cromosomes;
    }

    public int GetGene(int index){
        if(OutOfRange(index)){
            return -1;    
        }
        return this.genes[index];
    }

    public int[] GetGenes()
    {
        return this.genes;
    }

    public bool OutOfRange(int index){
        return index < 0 || index > genes.Length - 1;
    }

    public bool IsGeneOk(int gene_value){
        return gene_value >= 0 && gene_value <= max_gene;
    }

}
