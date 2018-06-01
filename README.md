# ACE! NEW! FINE?

Welcome to ACE! NEW! FINE?

What is 'ACE! NEW! FINE?'? It is a 'brand new' (tm, full literature review not completed!) algorithm which incorporates a ton of techniques talked about this week. It stands for:

"Adversarial Co-Evolution of Neuro-Evolution with Feasible-Infeasible Evolution"

Yeah ... that is a lot of buzz words and a lot of the uses of "evolution". This was 100% because we fell it accurately explains our algorithm and not at all because it spells a few words ... honest. 

Let's break down the algorithm

## Ace Curriculum Learning
Let's say you have a complicated problem, for example driving a car (no idea where we got this inspiration from...), and you want to train your agents to be good at it but the problem is, like, really hard. So, you want to train it is solve a simpler problem first, right? Great, so you can just design some simpler tracks and everyone is happy. But what if you are lazy like we are and you don't want to bother designing these easy tracks? Perhaps it would be amazing if you could design an agent to generate tracks which are *always* a little harder than the AI driving the cars would really like. Sounds good, right? This is the motivation for ACE! NEW! FINE?

This is implemented by developing a fitness function for the track AI based on the performance of all cars and a fitness function for the car AI based on the performance of that car on all tracks. Formally:

Ft = 1/Nc * SUM(Progress(ci,t) + 1-Time(ci,t))

Fc = 1/Nt * SUM(Progress(c,ti) + Time(c,ti))

Where Progress and Time are values between 0-1. Progress tracks how far along the track the car got and Time tracks for fast they completed the track (or 0 if incomplete). Both AIs are incentivised to have the cars finish the tracks but the track AI wants tracks which take the longest time to complete and the car AI wants cars which are as fast a possible. In this way, the challenge of the track should get easier when the AI is poor but once the cars finish the tracks the track AI is incentivised to make harder tracks until the cars can no-longer finish them.


## Neat Car Diving

Car Driving is pretty neat, right? In this project, we use NEAT (Neuro-Evolution of Augmented Topologies) to control the cars. This code is based on UnityNEAT - https://github.com/lordjesus/UnityNEAT (itself an implementation of SharpNEAT). However, we have had to make changes to the core code to get it working in the way we wanted so we can't promise our implementation of UnityNEAT/SharpNEAT will work for you!

## Evolving tracks (FI2POP)

