using AutoMapper;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using MOMShop.MomShopDbContext;

namespace MOMShop.Services.Implements
{
    public class ExportExcelService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ExportExcelService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void CreateExcelFile(string filePath)
        {
            // Tạo một tệp Excel mới
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                // Tạo phần WorkbookPart và thêm nó vào tài liệu
                WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                // Tạo phần WorksheetPart và thêm nó vào WorkbookPart
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Thêm một trang tính vào WorkbookPart
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
                sheets.Append(sheet);

                // Tạo một ô dữ liệu trong trang tính
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                Row row = new Row();
                Cell cell = new Cell() { CellValue = new CellValue("Hello, World!"), DataType = CellValues.String };
                row.Append(cell);
                sheetData.Append(row);

                // Lưu tài liệu
                workbookPart.Workbook.Save();
            }
        }
    }
}
