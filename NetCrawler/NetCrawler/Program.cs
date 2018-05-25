using System;

namespace NetCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello satyr!");
            Crawler.Girl13Crawler.CrawlHostMovieInfo();
            while(Console.ReadKey().KeyChar != 'q'){
                
            }
        }
    }
}
