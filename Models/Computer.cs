namespace LabManager.Models;

class Computer
{
    public int Id { get; set; }
    public string Ram { get; set; }
    public string Processor { get; set; }
    
    public Computer(int id, string ram, string processor)
    {
        Id = id;
        Ram = ram;
        Processor = processor;
    }
    
    
}