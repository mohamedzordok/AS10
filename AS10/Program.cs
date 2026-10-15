using System;

namespace SmartDelivery
{
    internal class Program
    {
        static void Main()
        {
            DeliveryUtilities.PrintSystemTitle();
            Console.WriteLine();

            // First access to a static member triggers the static constructor automatically.
            int before = Shipment.GetTotalShipmentsCreated();
            Console.WriteLine();

            // ---------------- Creating shipments ----------------
            DeliveryUtilities.PrintSystemTitle("Creating Shipments...");
            Console.WriteLine();

            Shipment s1 = new Shipment("SH001", "Standard", 3, new DeliveryAddress("Cairo", "Tahrir St"), "In Transit");
            Shipment s2 = new Shipment("SH002", "Express", 2, new DeliveryAddress("Alexandria", "Corniche"), "Out For Delivery");
            Shipment s3 = new Shipment("SH003", "International", 8, new DeliveryAddress("Dubai", "Sheikh Zayed Rd"), "Delivered");

            Console.WriteLine();
            Console.WriteLine($"Total Shipments Created : {Shipment.GetTotalShipmentsCreated()}");
            Console.WriteLine();

            // ---------------- Object copying ----------------
            DeliveryUtilities.PrintSystemTitle("Object Copying");
            Console.WriteLine();

            Shipment assigned = s1;                 // copies the REFERENCE only
            Console.WriteLine($"Original Shipment  : {s1.TrackingCode}");
            Console.WriteLine($"Assigned Shipment  : {assigned.TrackingCode}");
            Console.WriteLine();
            Console.WriteLine($"Same Object : {ReferenceEquals(s1, assigned)}");

            assigned.TrackingCode = "SH001-X";      // change through the second variable...
            Console.WriteLine($"After changing assigned.TrackingCode -> original is now: {s1.TrackingCode}");
            assigned.TrackingCode = "SH001";        // restore

            Shipment real = s1.CopyShipment();      // a real, separate object
            Console.WriteLine($"Same Object (CopyShipment) : {ReferenceEquals(s1, real)}");
            Console.WriteLine();

            // ---------------- Shallow copy ----------------
            DeliveryUtilities.PrintSystemTitle("Shallow Copy", '-');
            Console.WriteLine();

            Shipment shallow = s1.ShallowCopy();
            Console.WriteLine($"Original Shipment Address : {s1.DeliveryAddress.City}");
            Console.WriteLine($"Copied Shipment Address   : {shallow.DeliveryAddress.City}");
            Console.WriteLine();
            Console.WriteLine("Changing copied shipment address...");
            shallow.DeliveryAddress.City = "Giza";
            Console.WriteLine();
            Console.WriteLine($"Original Shipment Address : {s1.DeliveryAddress.City}");
            Console.WriteLine($"Copied Shipment Address   : {shallow.DeliveryAddress.City}");
            Console.WriteLine();
            Console.WriteLine($"Same Shipment Object        : {ReferenceEquals(s1, shallow)}");
            Console.WriteLine($"Same DeliveryAddress Object : {ReferenceEquals(s1.DeliveryAddress, shallow.DeliveryAddress)}");
            Console.WriteLine();

            // The shallow demo changed the shared address, so restore it before the deep demo.
            s1.DeliveryAddress.City = "Cairo";

            // ---------------- Deep copy ----------------
            DeliveryUtilities.PrintSystemTitle("Deep Copy", '-');
            Console.WriteLine();

            Shipment deep = s1.DeepCopy();
            Console.WriteLine($"Original Shipment Address : {s1.DeliveryAddress.City}");
            Console.WriteLine($"Copied Shipment Address   : {deep.DeliveryAddress.City}");
            Console.WriteLine();
            Console.WriteLine("Changing copied shipment address...");
            deep.DeliveryAddress.City = "Giza";
            Console.WriteLine();
            Console.WriteLine($"Original Shipment Address : {s1.DeliveryAddress.City}");
            Console.WriteLine($"Copied Shipment Address   : {deep.DeliveryAddress.City}");
            Console.WriteLine();
            Console.WriteLine($"Same DeliveryAddress Object : {ReferenceEquals(s1.DeliveryAddress, deep.DeliveryAddress)}");
            Console.WriteLine();

            // ---------------- Extension methods ----------------
            DeliveryUtilities.PrintSystemTitle("Extension Methods");
            Console.WriteLine();

            Console.WriteLine(s1.GetSummary());
            Console.WriteLine(s2.GetSummary());
            Console.WriteLine(s3.GetSummary());
            Console.WriteLine();
            Console.WriteLine($"SH001 Is Delivered : {s1.IsDelivered()}");
            Console.WriteLine($"SH003 Is Delivered : {s3.IsDelivered()}");
            Console.WriteLine();

            // ---------------- Tracking status ----------------
            DeliveryUtilities.PrintSystemTitle("Tracking Status");
            Console.WriteLine();
            s2.UpdateTrackingStatus("Out For Delivery");
            Console.WriteLine();

            // ---------------- Static utilities ----------------
            DeliveryUtilities.PrintSystemTitle("Static Utilities");
            Console.WriteLine();
            DeliveryUtilities.PrintSystemTitle("Delivery Center", '-');
            Console.WriteLine();
            // Copies made with MemberwiseClone() do not run the constructor, so the counter stays 3.
            Console.WriteLine($"Total Shipments Created : {Shipment.GetTotalShipmentsCreated()}");
            Console.WriteLine();

            // ---------------- Partial method ----------------
            DeliveryUtilities.PrintSystemTitle("Partial Method");
            Console.WriteLine();
            s3.UpdateTrackingStatus("Delivered");
            Console.WriteLine();

            DeliveryUtilities.PrintSystemTitle("Assignment Completed");
        }
    }
}
