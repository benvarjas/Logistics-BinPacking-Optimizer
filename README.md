# Logistics Bin Packing Optimizer (C#)

An industrial-grade, object-oriented C# solution designed to solve the classic **1D Bin Packing Problem** using the **First-Fit Decreasing (FFD)** heuristic. This engine optimization algorithm is highly applicable in real-world supply chain management, cargo loading optimization, and cloud computing resource allocation.

## Key Features
- **Object-Oriented Design (OOD):** Clean separation of concerns with dedicated entities for Cargo Items, Storage Bins, and the Optimization Service.
- **LINQ Integration:** Leveraging modern C# features for highly efficient, clean data filtering and aggregation.
- **Robust Error Handling:** Integrated validation checks to prevent illegal allocations (e.g., items exceeding maximum bin parameters).

## Architecture & Algorithm
The core service utilizes the **First-Fit Decreasing** heuristic, which sorts the cargo manifest in descending order based on weight before allocation. This minimizes the total number of containers required and maximizes space utilization efficiency.

## Technical Specifications
- **Language:** C# 10.0+
- **Framework:** .NET 6.0 / .NET 8.0 Console Application
- **Paradigm:** Object-Oriented Programming & Functional Data Manipulation (LINQ)
