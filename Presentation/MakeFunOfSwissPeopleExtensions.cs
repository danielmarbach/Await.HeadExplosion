using System;

static class MakeFunOfSwissPeopleExtensions
{
    public static void PrintJoke(this MakeFunOfSwissPeople runnable)
    {
        var index = random.Next(0, jokes.Length);
        Console.WriteLine(jokes[index]);
        Console.WriteLine();
    }

    static Random random = new Random();

    static string[] jokes = new string[] {
        "You know you're Swiss when you're train arrives three minutes late and the entire train station complains about it.",
        "Somebody asked Roger Federer what was good about being Swiss. He replied, 'Well, the flag is a big plus!'",
        @"What is this:

BANG!

.
.

BANG!

.
.

BANG!

.
.

BANG!


Answer: a Bernese machine gun.",
        @"What is the differerence between the Swiss and the Germans?

The Swiss are just like the Germans but without the sense of humour!!"
    };
}