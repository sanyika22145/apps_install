using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RejtvenyApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const int Rows = 20;
    private const int Columns = 10;
    private const double CellSize = 35;

    public MainWindow()
    {
        InitializeComponent();
        DrawGrid();
    }

    private void DrawGrid()
    {
        var blackCells = LoadBlackCells();

        PuzzleGrid.Rows = Rows;
        PuzzleGrid.Columns = Columns;
        PuzzleGrid.Children.Clear();

        for (var r = 0; r < Rows; r++)
        {
            for (var c = 0; c < Columns; c++)
            {
                var isBlack = blackCells.Contains((r, c));
                var box = new TextBox
                {
                    Width = CellSize,
                    Height = CellSize,
                    TextAlignment = TextAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    FontSize = 16
                };

                if (isBlack)
                {
                    box.Background = Brushes.Black;
                    box.Foreground = Brushes.Black;
                    box.IsReadOnly = true;
                    box.Focusable = false;
                }

                PuzzleGrid.Children.Add(box);
            }
        }
    }

    private static HashSet<(int Row, int Col)> LoadBlackCells()
    {
        var result = new HashSet<(int Row, int Col)>();
        const string fileName = "feketenegyzetek.txt";

        if (!File.Exists(fileName))
        {
            return result;
        }

        foreach (var line in File.ReadAllLines(fileName))
        {
            var parts = line.Split(',');
            if (parts.Length != 2)
            {
                continue;
            }

            if (int.TryParse(parts[0], out var row) && int.TryParse(parts[1], out var col))
            {
                result.Add((row, col));
            }
        }

        return result;
    }
}
