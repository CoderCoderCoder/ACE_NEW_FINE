# ACE? NEW? FINE?

Welcome to ACE? NEW? FINE?

What is 'ACE? NEW? FINE?'? It is a 'brand new' (tm, full literature review not completed!) algorithm which incorporates a ton of techniques talked about this week. It stands for:

"Adversarial Co-Evolution of Neuro-Evolution with Feasible-Infeasible Evolution"

Yeah ... that is a lot of buzz words and a lot of the uses of "evolution". This was 100% because we fell it accurately explains our algorithm and not at all because it spells a few words ... honest. We have been inspired by Julian's work on both neuro-evolution for Car Driving for the car driving AI and Feasible-Infeasible search for PCG for the track generating AI. We were also inspired by Arthur's discussion of curriculum learning to design the top-level architecture of the system.

Let's break down the algorithm:

## Ace Curriculum Learning
Let's say you have a complicated problem, for example driving a car (no idea where we got this inspiration from...), and you want to train your agents to be good at it but the problem is, like, really hard. So, you want to train it is solve a simpler problem first, right? Great, so you can just design some simpler tracks and everyone is happy. But what if you are lazy like we are and you don't want to bother designing these easy tracks? Perhaps it would be amazing if you could design an agent to generate tracks which are *always* a little harder than the AI driving the cars would really like. Sounds good, right? This is the motivation for ACE! NEW! FINE?

This is implemented by developing a fitness function for the track AI based on the performance of all cars and a fitness function for the car AI based on the performance of that car on all tracks. Formally:

Ft = 1/Nc * SUM(Progress(ci,t) + 1-Time(ci,t))

Fc = 1/Nt * SUM(Progress(c,ti) + Time(c,ti))

Where Progress and Time are values between 0-1. Progress tracks how far along the track the car got and Time tracks for fast they completed the track (or 0 if incomplete). Both AIs are incentivised to have the cars finish the tracks but the track AI wants tracks which take the longest time to complete and the car AI wants cars which are as fast a possible. In this way, the challenge of the track should get easier when the AI is poor but once the cars finish the tracks the track AI is incentivised to make harder tracks until the cars can no-longer finish them.

Is this really curriculum learning? I am not sure, I am no expert in the field. But if it is not I like the idea of calling it Teacher-Student training where the Teacher (the track generating AI) is trying to understand how good the student (the car driving AI) is and adjust its content so it is always challenging the student but not by a lot, getting more difficult as the student improves. 


## Neat Car Diving

Car Driving is pretty neat, right? In this project, we use NEAT (Neuro-Evolution of Augmented Topologies) ([paper](http://nn.cs.utexas.edu/downloads/papers/stanley.ec02.pdf)) to control the cars. This code is based on [UnityNEAT](https://github.com/lordjesus/UnityNEAT)(itself an implementation of SharpNEAT). However, we have had to make changes to the core code to get it working in the way we wanted so we can't promise our implementation of UnityNEAT/SharpNEAT will work for you!

NEAT uses Neuro-Evolution not just learn the weights of the Neural Network but also what it's topology (the number of neurons and connection). In our case, we have the standard 5 inputs – ray-casts forward, forward-left, forward-right, left and right - and 2 outputs - one which controls how much the car should turn and one which controls if the car should accelerate or break and by how much. 

## Fine Tracks

The FI2POP is an implementation of the Feasible-Infeasible Two-Population Genetic Algorithm ([paper](https://repository.upenn.edu/cgi/viewcontent.cgi?article=1269&context=oid_papers)). This algorithm can be used in scenarios where the population evolved is subject to some form of constraints. The algorithm takes care of evaluating the feasibility of each chromosome of the population and separate them in two different populations. To create the offspring of a new generation the chromosomes are mated within their own sub-population and then they are evaluated for feasibility and split again in the two sub-populations. This algorithm promotes a richer gene variety since it stores in a separate population the infeasible but potentially rich and valuable genetic material without affecting the evolution of the feasible individuals. In such algorithm, there's a constant migration of individuals from a sub-population to the other.

The chromosome representation is basically a sequence of squared track portions: straight track, smooth turn and sharp turn.
The evolution does the rest!

But enough with the boring stuff!
The FI2POP algorithm separates the broken tracks from the ones that we can actually play, the poor NEAT cars really want to show how they are poor at driving, right? Basically, any chromosome that describes a track that overlaps with itself is labeld as infeasible and segregatet in the bad-boy club. To check for overlaps we start building the track from the first gene and then procede gene-by-gene, as soon as we find a gene that overlaps with a previous one, the chromosome is marked as unfeasible!

## Running this tech demo
If you want to run this tech demo and see ACE? NEW? FINE? for yourself download this repo, open it in Unity (it was developed in 2018.1.1), load the scene (there is only one), run and then once the game has started press "Start EA". 

## Team

Charlie Ringer

Ivan Bravi

Cristiana Pacheco



