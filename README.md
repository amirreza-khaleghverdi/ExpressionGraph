# 📐 Expression Graph Calculator

> A modern WPF calculator that visualizes mathematical expressions as dynamic graphs, built with MVVM architecture and customizable operator priorities.

[![Download](https://img.shields.io/badge/Download-Latest%20Release-brightgreen?style=for-the-badge)](../../releases/latest)

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge\&logo=c-sharp\&logoColor=white)
![WPF](https://img.shields.io/badge/WPF-5C2D91?style=for-the-badge\&logo=.net\&logoColor=white)
![MVVM](https://img.shields.io/badge/MVVM-FF6F00?style=for-the-badge\&logo=github\&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge\&logo=dotnet\&logoColor=white)

---

## 📋 Table of Contents

* [Overview](#-overview)
* [Features](#-features)
* [Architecture](#-architecture)
* [Tech Stack](#-tech-stack)
* [Getting Started](#-getting-started)
* [How to Use](#-how-to-use)
* [Screenshots](#-screenshots)
* [Data Structures Concepts](#-data-structures-concepts)
* [Future Improvements](#-future-improvements)
* [Contributing](#-contributing)
* [License](#-license)

---

## 🎯 Overview

**Expression Graph Calculator** is more than a traditional calculator. It evaluates mathematical expressions while simultaneously visualizing the computation process as a graph structure. Built with **WPF** and **MVVM architecture**, it demonstrates how data structures and graph-based algorithms can be applied to real-world software applications.

This project was developed as part of a **Data Structures and Algorithms** course and focuses on expression parsing, operator precedence management, graph generation, and interactive visualization.

---

## ✨ Features

### 🧮 Smart Calculation Engine

* Supports arithmetic operators: `+`, `-`, `*`, `/`, `^`
* Customizable operator precedence
* Parentheses and nested expression support
* Expression parsing and evaluation
* Input validation and error handling

### 📊 Graph Visualization

* Visualizes expressions as graph/tree structures
* Nodes represent values and operations
* Edges represent relationships and evaluation flow
* Interactive graph display
* Clear visualization of expression evaluation

### 🎨 UI / UX

* Built using the **MVVM architectural pattern**
* Modern WPF interface
* Dark theme design
* Smooth transitions and animations
* Optional background music support

### ⚙️ Customizable Priorities

Users can dynamically change operator precedence rules and instantly observe how the resulting expression graph and final calculation are affected.

---

## 🧠 Architecture

```text
┌─────────────────────────────────────────────────────┐
│                       View                          │
│              (XAML + Data Binding)                  │
├─────────────────────────────────────────────────────┤
│                    ViewModel                        │
│           (Commands, State, Logic)                  │
├─────────────────────────────────────────────────────┤
│                     Model                           │
│      (Expression Evaluator + Graph Engine)          │
└─────────────────────────────────────────────────────┘
```

## 📚 Data Structures Concepts

This project applies several core DSA concepts:

* Expression Trees
* Graph Traversal
* Parsing Algorithms
* Priority Management
* Tree-Based Expression Evaluation

### Components

| Layer         | Responsibility                                                               |
| ------------- | ---------------------------------------------------------------------------- |
| **Model**     | Expression parser, operator priority system, graph generation algorithm      |
| **ViewModel** | Connects UI and business logic using `INotifyPropertyChanged` and `ICommand` |
| **View**      | XAML-based user interface with data binding and animations                   |

---

## 🛠️ Tech Stack

| Component            | Technology                               |
| -------------------- | ---------------------------------------- |
| UI Framework         | WPF (.NET)                               |
| Architecture         | MVVM                                     |
| Language             | C#                                       |
| Graph Rendering      | Custom Canvas / LiveCharts               |
| Audio                | MediaPlayer / NAudio                     |
| Dependency Injection | Microsoft.Extensions.DependencyInjection |

---

## 🚀 Getting Started

### Prerequisites

* Windows 10/11
* .NET 6.0+ or .NET Framework 4.7.2+
* Visual Studio 2022 or later

### Installation

```bash
git clone https://github.com/amirreza-khaleghverdi/ExpressionGraph.git

cd ExpressionGraph

dotnet build

dotnet run
```

Alternatively, open the solution file in Visual Studio and press **F5**.

---

## 🎮 How to Use

1. Enter a mathematical expression

   Example:

   ```text
   (3 + 5) * 2 ^ 3
   ```

2. Click **Calculate**

3. View the generated graph structure

4. Modify operator priorities using the Priority Editor

5. Recalculate and compare the results

6. Enjoy optional background music while exploring expressions

---

## 📸 Screenshots

### Main Calculator View

![Main View](screenshots/calculator-main.png)

### Graph Visualization

![Graph View](screenshots/graph-view.png)

### Priority Editor

![Priority Editor](screenshots/priority-editor.png)

### Variable Editor

![Variable Editor](screenshots/variable-editor.png)

---

## 🎵 Music Credit

Background music: **Aria Math** by **C418**

Included as part of the application's user experience and used for educational purposes.

---

## 📌 Future Improvements

* [ ] Save graphs as PNG/SVG
* [ ] Export calculations as CSV
* [ ] Support mathematical functions (`sin`, `cos`, `log`, `sqrt`)
* [ ] Light theme support
* [ ] Additional unit tests
* [ ] Keyboard shortcuts
* [ ] Calculation history
* [ ] Expression import/export

---

## 🤝 Contributing

Contributions, suggestions, and improvements are welcome.

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push the branch
5. Open a Pull Request

---

## 📄 License

MIT License © Amirreza Khaleghverdi

---

## 🔗 Project Repository

Repository:

https://github.com/amirreza-khaleghverdi/ExpressionGraph

---

**Made with ❤️, C#, WPF, and a passion for Data Structures & Algorithms**
