using System;
using System.Collections.Generic;
using System.Linq;

namespace LogisticsOptimizer
{
    // A szállítandó csomagot reprezentáló osztály
    public class CargoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; } // kg

        public CargoItem(int id, string name, double weight)
        {
            Id = id;
            Name = name;
            Weight = weight;
        }
    }

    // A konténert/ládát reprezentáló osztály
    public class Bin
    {
        public int Id { get; set; }
        public double MaxCapacity { get; set; } // kg
        public List<CargoItem> PackedItems { get; private set; }

        public double CurrentWeight => PackedItems.Sum(item => item.Weight);
        public double RemainingCapacity => MaxCapacity - CurrentWeight;

        public Bin(int id, double maxCapacity)
        {
            Id = id;
            MaxCapacity = maxCapacity;
            PackedItems = new List<CargoItem>();
        }

        public bool TryAddItem(CargoItem item)
        {
            if (RemainingCapacity >= item.Weight)
            {
                PackedItems.Add(item);
                return true;
            }
            return false;
        }
    }

    // A logisztikai algoritmust futtató szerviz (First-Fit Decreasing)
    public class BinPackingService
    {
        public List<Bin> OptimizeStorage(List<CargoItem> items, double binCapacity)
        {
            if (items == null || !items.Any()) return new List<Bin>();

            // Algoritmus alapja: Csökkenő súly szerint sorba rendezzük a csomagokat a maximális helykihasználáshoz
            var sortedItems = items.OrderByDescending(i => i.Weight).ToList();
            var bins = new List<Bin>();
            int binCounter = 1;

            foreach (var item in sortedItems)
            {
                if (item.Weight > binCapacity)
                {
                    throw new InvalidOperationException($"Item {item.Name} (Weight: {item.Weight}kg) exceeds the maximum capacity of a single bin ({binCapacity}kg).");
                }

                bool packed = false;

                // Megpróbáljuk berakni egy már meglévő ládába, amiben van elég hely
                foreach (var bin in bins)
                {
                    if (bin.TryAddItem(item))
                    {
                        packed = true;
                        break;
                    }
                }

                // Ha egyik meglévő ládába sem fért be, nyitunk egy újat
                if (!packed)
                {
                    var newBin = new Bin(binCounter++, binCapacity);
                    newBin.TryAddItem(item);
                    bins.Add(newBin);
                }
            }

            return bins;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Logistics Fleet & Bin Packing Optimizer ===");

            // Teszt adatok: Különböző súlyú szállítmányok
            var cargoManifest = new List<CargoItem>
            {
                new CargoItem(1, "Industrial Component A", 450),
                new CargoItem(2, "Server Rack Unit", 300),
                new CargoItem(3, "Raw Materials Pallet", 600),
                new CargoItem(4, "Medical Equipment Type B", 150),
                new CargoItem(5, "Spare Parts Box", 200),
                new CargoItem(6, "Heavy Machinery Gear", 700),
                new CargoItem(7, "Lithium Battery Pack", 350)
            };

            double standardBinCapacity = 1000; // 1 tonnás konténerek
            var optimizer = new BinPackingService();

            try
            {
                var optimizedBins = optimizer.OptimizeStorage(cargoManifest, standardBinCapacity);

                // Eredmények professzionális konzolos kiíratása
                Console.WriteLine($"\nOptimization Complete. Total Bins Required: {optimizedBins.Count}\n");

                foreach (var bin in optimizedBins)
                {
                    Console.WriteLine($"-------------------------------------------------- Built-in Bin #{bin.Id} --------------------------------------------------");
                    Console.WriteLine($"Capacity Utilization: {bin.CurrentWeight} / {bin.MaxCapacity} kg ({Math.Round((bin.CurrentWeight / bin.MaxCapacity) * 100, 2)}%)");
                    Console.WriteLine("Packed Items:");
                    
                    foreach (var item in bin.PackedItems)
                    {
                        Console.WriteLine($"  -> [ID: {item.Id}] {item.Name} - {item.Weight} kg");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Optimization Failed: {ex.Message}");
            }
        }
    }
}
