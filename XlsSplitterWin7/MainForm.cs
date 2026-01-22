using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace XlsSplitterWin7
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 97-2003 (*.xls)|*.xls";
                dialog.Title = "Select an Excel .xls file";
                dialog.Multiselect = false;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    selectedFileTextBox.Text = dialog.FileName;
                    splitButton.Enabled = true;
                    statusLabel.Text = "Ready to split.";
                }
            }
        }

        private void SplitButton_Click(object sender, EventArgs e)
        {
            string inputPath = selectedFileTextBox.Text;
            if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
            {
                MessageBox.Show(this, "Please select a valid .xls file.", "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                statusLabel.Text = "Reading workbook...";
                using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    HSSFWorkbook inputWorkbook = new HSSFWorkbook(inputStream);
                    ISheet inputSheet = inputWorkbook.GetSheetAt(0);
                    if (inputSheet == null)
                    {
                        MessageBox.Show(this, "The workbook does not contain any sheets.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    HSSFWorkbook positiveWorkbook = new HSSFWorkbook();
                    HSSFWorkbook negativeWorkbook = new HSSFWorkbook();
                    ISheet positiveSheet = positiveWorkbook.CreateSheet(inputSheet.SheetName);
                    ISheet negativeSheet = negativeWorkbook.CreateSheet(inputSheet.SheetName);

                    int positiveRowIndex = 0;
                    int negativeRowIndex = 0;

                    IRow headerRow = inputSheet.GetRow(0);
                    if (headerRow != null)
                    {
                        CopyRowValues(headerRow, positiveSheet.CreateRow(positiveRowIndex++));
                        CopyRowValues(headerRow, negativeSheet.CreateRow(negativeRowIndex++));
                    }

                    int lastRow = inputSheet.LastRowNum;
                    for (int rowIndex = 1; rowIndex <= lastRow; rowIndex++)
                    {
                        IRow inputRow = inputSheet.GetRow(rowIndex);
                        if (inputRow == null)
                        {
                            continue;
                        }

                        ICell targetCell = inputRow.GetCell(5);
                        if (!TryGetNumericCellValue(targetCell, out double value))
                        {
                            continue;
                        }

                        if (value > 0)
                        {
                            CopyRowValues(inputRow, positiveSheet.CreateRow(positiveRowIndex++));
                        }
                        else if (value < 0)
                        {
                            CopyRowValues(inputRow, negativeSheet.CreateRow(negativeRowIndex++));
                        }
                    }

                    string folder = Path.GetDirectoryName(inputPath) ?? string.Empty;
                    string baseName = Path.GetFileNameWithoutExtension(inputPath);
                    string positivePath = Path.Combine(folder, baseName + "_positive.xls");
                    string negativePath = Path.Combine(folder, baseName + "_negative.xls");

                    statusLabel.Text = "Writing output files...";
                    using (FileStream outputPositive = new FileStream(positivePath, FileMode.Create, FileAccess.Write))
                    {
                        positiveWorkbook.Write(outputPositive);
                    }

                    using (FileStream outputNegative = new FileStream(negativePath, FileMode.Create, FileAccess.Write))
                    {
                        negativeWorkbook.Write(outputNegative);
                    }

                    statusLabel.Text = "Split complete.";
                    MessageBox.Show(this,
                        "Split complete!\n" +
                        "Positive rows: " + positivePath + "\n" +
                        "Negative rows: " + negativePath,
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to split file.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error encountered.";
            }
        }

        private static bool TryGetNumericCellValue(ICell cell, out double value)
        {
            value = 0;
            if (cell == null)
            {
                return false;
            }

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    value = cell.NumericCellValue;
                    return true;
                case CellType.String:
                    return TryParseNumeric(cell.StringCellValue, out value);
                case CellType.Formula:
                    try
                    {
                        value = cell.NumericCellValue;
                        return true;
                    }
                    catch
                    {
                        return TryParseNumeric(cell.ToString(), out value);
                    }
                case CellType.Blank:
                    return false;
                default:
                    return TryParseNumeric(cell.ToString(), out value);
            }
        }

        private static bool TryParseNumeric(string text, out double value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            return double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value)
                || double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out value);
        }

        private static void CopyRowValues(IRow sourceRow, IRow targetRow)
        {
            if (sourceRow == null || targetRow == null)
            {
                return;
            }

            for (int cellIndex = 0; cellIndex < sourceRow.LastCellNum; cellIndex++)
            {
                ICell sourceCell = sourceRow.GetCell(cellIndex);
                if (sourceCell == null)
                {
                    continue;
                }

                ICell targetCell = targetRow.CreateCell(cellIndex);
                switch (sourceCell.CellType)
                {
                    case CellType.Numeric:
                        targetCell.SetCellValue(sourceCell.NumericCellValue);
                        break;
                    case CellType.String:
                        targetCell.SetCellValue(sourceCell.StringCellValue);
                        break;
                    case CellType.Boolean:
                        targetCell.SetCellValue(sourceCell.BooleanCellValue);
                        break;
                    case CellType.Formula:
                        try
                        {
                            targetCell.SetCellValue(sourceCell.NumericCellValue);
                        }
                        catch
                        {
                            targetCell.SetCellValue(sourceCell.ToString());
                        }
                        break;
                    default:
                        targetCell.SetCellValue(sourceCell.ToString());
                        break;
                }
            }
        }
    }
}
