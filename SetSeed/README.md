# SetSeed

Allows you to set the SessionSeed in game using `/setsessionseed`.

You can only do this as the Host.

## How to use

There are 2 ways to set the seed:

1. Open chat as the Host and type `/setsessionseed <seed>` where the seed is an integer.

2. Open chat as the Host and type `/setsessionseed`. This will set the seed to your copied clipboard if that value is a valid value.

Once you set the seed to a value a chat message will be sent with the new seed. The seed is saved 
and if you create another lobby the seed will be the same.

## Seeds that can be set:

- **SessionSeed:** This sets most of the RNG in a level such as keys, cells, objective items.
