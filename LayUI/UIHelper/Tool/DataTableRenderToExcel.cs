using NPOI.HSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
namespace UIHelper
{
	internal class DataTableRenderToExcel
	{
		public static Stream RenderDataTableToExcel(DataTable SourceTable)
		{
			HSSFWorkbook hSSFWorkbook = new HSSFWorkbook();
			MemoryStream memoryStream = new MemoryStream();
			HSSFSheet hSSFSheet = (HSSFSheet)hSSFWorkbook.CreateSheet();
			HSSFRow hSSFRow = (HSSFRow)hSSFSheet.CreateRow(0);
			foreach (DataColumn dataColumn in SourceTable.Columns)
			{
				hSSFRow.CreateCell(dataColumn.Ordinal).SetCellValue(dataColumn.ColumnName);
			}
			int num = 1;
			foreach (DataRow dataRow in SourceTable.Rows)
			{
				HSSFRow hSSFRow2 = (HSSFRow)hSSFSheet.CreateRow(num);
				foreach (DataColumn dataColumn2 in SourceTable.Columns)
				{
					hSSFRow2.CreateCell(dataColumn2.Ordinal).SetCellValue(dataRow[dataColumn2].ToString());
				}
				num++;
			}
			hSSFWorkbook.Write(memoryStream);
			memoryStream.Flush();
			memoryStream.Position = 0L;
			hSSFSheet = null;
			hSSFWorkbook = null;
			return memoryStream;
		}
		public static void RenderDataTableToExcel(DataTable SourceTable, string FileName)
		{
			MemoryStream memoryStream = DataTableRenderToExcel.RenderDataTableToExcel(SourceTable) as MemoryStream;
			FileStream fileStream = new FileStream(FileName, FileMode.Create, FileAccess.Write);
			byte[] array = memoryStream.ToArray();
			fileStream.Write(array, 0, array.Length);
			fileStream.Flush();
			fileStream.Close();
		}
		public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, string SheetName, int HeaderRowIndex)
		{
			HSSFWorkbook hSSFWorkbook = new HSSFWorkbook(ExcelFileStream);
			HSSFSheet hSSFSheet = (HSSFSheet)hSSFWorkbook.GetSheet(SheetName);
			DataTable dataTable = new DataTable();
			HSSFRow hSSFRow = (HSSFRow)hSSFSheet.GetRow(HeaderRowIndex);
			int lastCellNum = hSSFRow.LastCellNum;
			for (int i = hSSFRow.FirstCellNum; i < lastCellNum; i++)
			{
				DataColumn column = new DataColumn(hSSFRow.GetCell(i).StringCellValue);
				dataTable.Columns.Add(column);
			}
			int lastRowNum = hSSFSheet.LastRowNum;
			for (int j = hSSFSheet.FirstRowNum + 1; j < hSSFSheet.LastRowNum; j++)
			{
				HSSFRow hSSFRow2 = (HSSFRow)hSSFSheet.GetRow(j);
				DataRow dataRow = dataTable.NewRow();
				for (int k = hSSFRow2.FirstCellNum; k < lastCellNum; k++)
				{
					dataRow[k] = hSSFRow2.GetCell(k).ToString();
				}
			}
			ExcelFileStream.Close();
			return dataTable;
		}
		public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
		{
			HSSFWorkbook hSSFWorkbook = new HSSFWorkbook(ExcelFileStream);
			HSSFSheet hSSFSheet = (HSSFSheet)hSSFWorkbook.GetSheetAt(SheetIndex);
			DataTable dataTable = new DataTable();
			HSSFRow hSSFRow = (HSSFRow)hSSFSheet.GetRow(HeaderRowIndex);
			int lastCellNum = hSSFRow.LastCellNum;
			for (int i = hSSFRow.FirstCellNum; i < lastCellNum; i++)
			{
				DataColumn column = new DataColumn(hSSFRow.GetCell(i).StringCellValue);
				dataTable.Columns.Add(column);
			}
			int lastRowNum = hSSFSheet.LastRowNum;
			for (int j = hSSFSheet.FirstRowNum + 1; j < hSSFSheet.LastRowNum; j++)
			{
				HSSFRow hSSFRow2 = (HSSFRow)hSSFSheet.GetRow(j);
				DataRow dataRow = dataTable.NewRow();
				for (int k = hSSFRow2.FirstCellNum; k < lastCellNum; k++)
				{
					bool flag = hSSFRow2.GetCell(k) != null;
					if (flag)
					{
						dataRow[k] = hSSFRow2.GetCell(k).ToString();
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			ExcelFileStream.Close();
			return dataTable;
		}
		public static DataTable RenderDataTableFromExcel(string path)
		{
			DataTable dataTable = new DataTable();
			HSSFWorkbook hSSFWorkbook;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				hSSFWorkbook = new HSSFWorkbook(fileStream);
			}
			HSSFSheet hSSFSheet = (HSSFSheet)hSSFWorkbook.GetSheetAt(0);
			IEnumerator rowEnumerator = hSSFSheet.GetRowEnumerator();
			HSSFRow hSSFRow = (HSSFRow)hSSFSheet.GetRow(0);
			int lastCellNum = hSSFRow.LastCellNum;
			for (int i = 0; i < lastCellNum; i++)
			{
				HSSFCell hSSFCell = (HSSFCell)hSSFRow.GetCell(i);
				bool flag = hSSFCell == null;
				if (flag)
				{
					dataTable.Columns.Add("");
				}
				else
				{
					dataTable.Columns.Add(hSSFCell.ToString());
				}
			}
			for (int j = hSSFSheet.FirstRowNum + 1; j <= hSSFSheet.LastRowNum; j++)
			{
				HSSFRow hSSFRow2 = (HSSFRow)hSSFSheet.GetRow(j);
				bool flag2 = hSSFRow2 == null;
				if (!flag2)
				{
					DataRow dataRow = dataTable.NewRow();
					for (int k = hSSFRow2.FirstCellNum; k < lastCellNum; k++)
					{
						bool flag3 = hSSFRow2.GetCell(k) != null;
						if (flag3)
						{
							dataRow[k] = hSSFRow2.GetCell(k).ToString();
						}
					}
					dataTable.Rows.Add(dataRow);
				}
			}
			return dataTable;
		}
		public static DataTable RenderDataTableFromExcel(string path, int sheetnum)
		{
			DataTable dataTable = new DataTable();
			HSSFWorkbook hSSFWorkbook;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				hSSFWorkbook = new HSSFWorkbook(fileStream);
			}
			HSSFSheet hSSFSheet = (HSSFSheet)hSSFWorkbook.GetSheetAt(sheetnum);
			IEnumerator rowEnumerator = hSSFSheet.GetRowEnumerator();
			HSSFRow hSSFRow = (HSSFRow)hSSFSheet.GetRow(0);
			int lastCellNum = hSSFRow.LastCellNum;
			for (int i = 0; i < lastCellNum; i++)
			{
				HSSFCell hSSFCell = (HSSFCell)hSSFRow.GetCell(i);
				bool flag = hSSFCell == null;
				if (flag)
				{
					dataTable.Columns.Add("");
				}
				else
				{
					dataTable.Columns.Add(hSSFCell.ToString());
				}
			}
			for (int j = hSSFSheet.FirstRowNum + 1; j <= hSSFSheet.LastRowNum; j++)
			{
				HSSFRow hSSFRow2 = (HSSFRow)hSSFSheet.GetRow(j);
				bool flag2 = hSSFRow2 == null;
				if (!flag2)
				{
					DataRow dataRow = dataTable.NewRow();
					for (int k = hSSFRow2.FirstCellNum; k < lastCellNum; k++)
					{
						bool flag3 = hSSFRow2.GetCell(k) != null;
						if (flag3)
						{
							dataRow[k] = hSSFRow2.GetCell(k).ToString();
						}
					}
					dataTable.Rows.Add(dataRow);
				}
			}
			return dataTable;
		}
		public static DataTable RenderDataTableFromExcel(string path, string sheetname)
		{
			DataTable dataTable = new DataTable();
			HSSFWorkbook hSSFWorkbook;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				hSSFWorkbook = new HSSFWorkbook(fileStream);
			}
			HSSFSheet hSSFSheet = (HSSFSheet)hSSFWorkbook.GetSheet(sheetname);
			IEnumerator rowEnumerator = hSSFSheet.GetRowEnumerator();
			HSSFRow hSSFRow = (HSSFRow)hSSFSheet.GetRow(0);
			int lastCellNum = hSSFRow.LastCellNum;
			for (int i = 0; i < lastCellNum; i++)
			{
				HSSFCell hSSFCell = (HSSFCell)hSSFRow.GetCell(i);
				bool flag = hSSFCell == null;
				if (flag)
				{
					dataTable.Columns.Add("");
				}
				else
				{
					dataTable.Columns.Add(hSSFCell.ToString());
				}
			}
			for (int j = hSSFSheet.FirstRowNum + 1; j <= hSSFSheet.LastRowNum; j++)
			{
				HSSFRow hSSFRow2 = (HSSFRow)hSSFSheet.GetRow(j);
				bool flag2 = hSSFRow2 == null;
				if (!flag2)
				{
					DataRow dataRow = dataTable.NewRow();
					for (int k = hSSFRow2.FirstCellNum; k < lastCellNum; k++)
					{
						bool flag3 = hSSFRow2.GetCell(k) != null;
						if (flag3)
						{
							dataRow[k] = hSSFRow2.GetCell(k).ToString();
						}
					}
					dataTable.Rows.Add(dataRow);
				}
			}
			return dataTable;
		}
		internal static List<string> GetExcelSheet(string filepath)
		{
			HSSFWorkbook hSSFWorkbook;
			using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
			{
				hSSFWorkbook = new HSSFWorkbook(fileStream);
			}
			List<string> list = new List<string>();
			for (int i = 0; i < hSSFWorkbook.NumberOfSheets; i++)
			{
				list.Add(hSSFWorkbook.GetSheetName(i));
			}
			return list;
		}
	}
}
