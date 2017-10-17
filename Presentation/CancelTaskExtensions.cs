using System;

static class CancelTaskExtensions 
{
    public static void Explain(this CancelTask runnable)
    {
        Console.WriteLine(" # Passing a token to a task does only impact the final state of the task");
    }   
}