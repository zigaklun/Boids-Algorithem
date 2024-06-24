# Boids-Algorithem

Ta projekt implementira algoritem boidov v jeziku C# z uporabo WPF (Windows Presentation Foundation) za vizualizacijo.

## Opis

Algoritem boidov simulira vedenje jate ptic z uporabo preprostih pravil za vsak posamezen boid (ptič). Vsak boid se giblje na podlagi treh osnovnih principov:
- **Ločitev**: izogibanje trkom z bližnjimi boidi.
- **Usklajevanje**: poravnava smeri gibanja s sosednjimi boidi.
- **Kohesija**: premikanje proti povprečni poziciji sosednjih boidov.
Več o Boidovem algoritmu : https://en.wikipedia.org/wiki/Boids

## Zagon

1. Prenesite ali klonirajte ta repozitorij:
    ```bash
    git clone https://github.com/zigaklun/Boids-Algorithm.git
    ```
2. Namestite .NET 8.0 SDK 
3. V datoteki "Zgrajen Projekt" zaženite "BoidsAlgorith.exe". Odpru se bo kanvas z zagnano simulacijo.
   

## Struktura Projekta

- **MainWindow.xaml**: XAML datoteka, ki definira uporabniški vmesnik.
- **MainWindow.xaml.cs**: C# datoteka, ki vsebuje logiko za glavno okno.
- **Ptici.cs**: Razred, ki implementira posamezenega ptica (eng. boid).

## Avtor
Žiga Klun

