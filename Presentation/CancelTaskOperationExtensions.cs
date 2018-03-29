using System.IO;

static class CancelTaskOperationExtensions 
{
    public static void Explain(this CancelTaskOperation runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- Cooperative cancelation means the token has to be observed by the implementor
- The implementor decides what kind of guarantees it can provide
- For example HttpClient cancelation it can happen that side-effects still occur
");
    }   
}