using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Alba.CsConsoleFormat.Fluent;
using ConsoleTables;

namespace do_an_quan_ly_don_hang
{
    public class Utils
    {
        public static bool IsPropertyExist(dynamic settings, string name)
        {
            if (settings is ExpandoObject)
                return ((IDictionary<string, object>)settings).ContainsKey(name);

            return settings.GetType().GetProperty(name) != null;
        }
        public static bool HasProperty(ExpandoObject obj, string propertyName)
        {
            return ((IDictionary<String, object>)obj).ContainsKey(propertyName);
        }
    }
    public static class Constants
    {
        public const string MENU_START = "VUI LONG CHON NHUNG LUA CHON SAU DAY:";
        public const string MENU_CREATE_PRODUCT = "1: Tao moi hoac them san pham moi";
        public const string MENU_DELETE_PRODUCT_INFO = "2: Xoa san pham";
        public const string MENU_EDIT_PRODUCT_INFO = "3: Sua thong tin san pham";
        public const string MENU_FIND_PRODUCT_INFO = "4: Tim san pham theo ten";
        public const string MENU_VIEW_PRODUCT_INFO = "5: Xem tat ca san pham";
        public const string MENU_CREATE_PRODUCT_CATEGORY = "6: Tao moi hoac them loai hang moi";
        public const string MENU_DELETE_PRODUCT_CATEGORY_INFO = "7: Xoa loai hang";
        public const string MENU_EDIT_PRODUCT_CATEGORY_INFO = "8: Sua thong tin loai hang";
        public const string MENU_FIND_PRODUCT_CATEGORY_INFO = "9: Tim loai hang theo ten";
        public const string MENU_VIEW_PRODUCT_CATEGORY_INFO = "10: Xem tat ca loai hang";
        public const string MENU_LIST_PRODUCTS_STOCK_BY_CATEGORY = "11: Liet ke hang ton kho theo loai hang";
        public const string MENU_LIST_EXPIRED_PRODUCTS = "12: Liet ke hang het han su dung";
        public const string MENU_EXIT = "13: Thoat chuong trinh";
        public const string MENU_GOODBYE = "Tam biet!";
        public const string MENU_NOT_SUPPORT = "Lua chon nay chua duoc ho tro, xin vui long chon lai";
        public const string MENU_CLEAR_CONSOLE = "14: Clear man hinh";
        public const string END_OF_PAGE_MESSAGE = ">> NHAN MOT PHIM BAT KY DE TIEP TUC";


        public const string INPUT_PRODUCT_MESSAGE = "Nhap thong tin san pham: ";
        public const string INPUT_PRODUCT_NAME_MESSAGE = "Nhap ten san pham: ";
        public const string INPUT_EXPIRY_DATE_MESSAGE = "Nhap han su dung: ";
        public const string INPUT_MANUFACTURING_DATE_MESSAGE = "Nhap ngay san xuat: ";
        public const string INPUT_COMPANY_MESSAGE = "Nhap cong ty san xuat: ";
        public const string INPUT_CATEGORY_MESSAGE = "Nhap ten loai hang: ";
        public const string INPUT_CATEGORY_NAME_MESSAGE = "Nhap ten loai hang: ";
        public const string INPUT_STOCK_MESSAGE = "Nhap so luong ton: ";
        public const string INPUT_DELETE_PRODUCT_NAME = "Nhap ten san pham ban muon xoa: ";
        public const string INPUT_FIND_PRODUCT_NAME = "Nhap ten hoac id cua san pham ban muon tim kiem: ";
        public const string INPUT_EDIT_PRODUCT_NAME = "Nhap ten hoac id cua san pham ban muon cap nhat: ";
        public const string INPUT_EDIT_PRODUCT_PROPERTY = "Nhap thuoc tinh ma ban muon cap nhat: ";
        public const string INPUT_EDIT_VALUE = "Nhap gia tri moi cua {0}: ";
        public const string INPUT_FIND_PRODUCT_BY_CATEGORY = "Nhap ten loai hang can kiem tra ton kho, ten loai hang gom co: ";
        public const string INPUT_PRODUCT_CATEGORY_MESSAGE = "Nhap thong tin loai hang: ";
        public const string INPUT_PRODUCT_CATEGORY_NAME_MESSAGE = "Nhap ten loai hang: ";
        public const string INPUT_DELETE_PRODUCT_CATEGORY_NAME = "Nhap ten loai hang ma ban muon xoa: ";
        public const string INPUT_FIND_PRODUCT_CATEGORY_NAME = "Nhap ten hoac id cua loai hang  ban muon tim kiem: ";
        public const string INPUT_EDIT_PRODUCT_CATEGORY_NAME = "Nhap ten hoac id cua loai hang ban muon cap nhat: ";

        public const string STOCK_BY_CATEGORY = "Con lai {0} san pham thuoc mat hang {1} trong kho!";
        public const string EMPTY_PRODUCTS_CATEGORY = "Hien khong co loai san pham nao";
        public const string EMPTY_PRODUCTS = "Hien khong co san pham nao";
        public const string ERROR_NOT_SAVE_ANY_INFO = "Khong co ket qua nao";
        public const string ERROR_CAN_NOT_FIND = "cant find traiee info file";
        public const string ERROR_CAN_NOT_SAVE = "cant save traiee info file";
        public const string ERROR_ALREADY_SAVE = "you had already added {0} before";
        public const string ERROR_CAN_NOT_FILE_INFO = "{0} chua duoc them truoc day";
        public const string ERROR_CAN_NOT_REMOVE = "Khong the xoa {0}";
        public const string ERROR_CAN_NOT_REMOVE_DUE_TO_FOREIGN_KEY_IN_PRODUCT = "Khong the xoa vi co san pham thuoc loai hang nay";
        public const string SUCCESSFULL_ADD = "Them thanh cong!";
        public const string SUCCESSFULL_REMOVE = "Xoa thanh cong!";
        public const string SUCCESSFULL_EDIT = "Cap nhat thanh cong!";
        public const string EXCEPTION_INFO = "exception info";
        public const string HEAD_PRODUCT_INFO = "[ID] [ten san pham] [ngay san xuat] [cong ty san xuat] [loai hang] [han su dung]";
        public const string HEAD_PRODUCT_CATEGORY_INFO = "[ID] [ten loai hang]";
        public const string NO_CATEGORY_FOUND = "Khong co loai hang nao ten nay!";
        public const string PROPERTY_NOT_FOUND = "Thuoc tinh nay khong ton tai!";
        public const string FOUND_PRODUCTS_CATEGORY_RESULTS = "Tim duoc nhung loai hang sau:";
        public const string FOUND_PRODUCTS_RESULTS = "Tim duoc nhung san pham sau:";
    }

    [Serializable]
    public class ProductCategory
    {
        private string id;
        private string name;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public ProductCategory(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public ProductCategory()
        {
            this.id = string.Empty;
            this.name = string.Empty;
        }

        private string InputField(string displayMessage)
        {
            Console.Write(displayMessage);
            return Console.ReadLine();
        }

        public void InputProductCategoryInfo()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(Constants.INPUT_PRODUCT_CATEGORY_MESSAGE);
            this.name = InputField(Constants.INPUT_PRODUCT_CATEGORY_NAME_MESSAGE);
            Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
        }
    }

    class ProductCategoryInformation
    {
        private static ProductCategoryInformation productCategoryInformation;
        private Dictionary<string, ProductCategory> productDictionary;
        private readonly Dictionary<string, ProductCategory> productCategoryDictionary;
        private readonly BinaryFormatter formatter;
        private const string DATA_FILENAME = "productscategory.dat";
        public static ProductCategoryInformation Instance()
        {
            if (productCategoryInformation == null)
            {
                productCategoryInformation = new ProductCategoryInformation();
            }
            return productCategoryInformation;
        }
        private ProductCategoryInformation()
        {
            this.productCategoryDictionary = new Dictionary<string, ProductCategory>();
            this.formatter = new BinaryFormatter();
        }
        public bool Add(string name)
        {
            if (this.productCategoryDictionary.ContainsKey(name))
            {
                Console.WriteLine(String.Format(Constants.ERROR_ALREADY_SAVE, name));
                return false;
            }
            else
            {
                this.productCategoryDictionary.Add(name, new ProductCategory(Guid.NewGuid().ToString(), name));
                Console.WriteLine(Constants.SUCCESSFULL_ADD);
                return true;
            }

        }
        public bool Remove(string name)
        {
            ProductInformation product = ProductInformation.Instance();
            product.Load();
            if (!product.FindByCategory(name))
            {
                if (!this.productCategoryDictionary.ContainsKey(name))
                {
                    Console.WriteLine(String.Format(Constants.NO_CATEGORY_FOUND));
                    return false;
                }
                if (this.productCategoryDictionary.Remove(name))
                {
                    Console.WriteLine(String.Format(Constants.SUCCESSFULL_REMOVE, name));
                    return true;
                }
                else
                {
                    Console.WriteLine(String.Format(Constants.ERROR_CAN_NOT_REMOVE, name));
                    return false;
                }
            }
            else
            {
                Console.WriteLine(String.Format(Constants.ERROR_CAN_NOT_REMOVE_DUE_TO_FOREIGN_KEY_IN_PRODUCT));
                return false;
            }
            
        }
        public bool Save()
        {
            try
            {
                FileStream writerFileStream = new FileStream(DATA_FILENAME, FileMode.Create, FileAccess.Write);
                this.formatter.Serialize(writerFileStream, this.productCategoryDictionary);
                writerFileStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ERROR_CAN_NOT_SAVE);
                Console.WriteLine(ex.Message);
                return true;
            }
        }
        public bool Load()
        {
            if (File.Exists(DATA_FILENAME))
            {
                try
                {
                    FileStream readerFileStream = new FileStream(DATA_FILENAME, FileMode.Open, FileAccess.Read);
                    productDictionary = (Dictionary<String, ProductCategory>)formatter.Deserialize(readerFileStream);
                    readerFileStream.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(Constants.EXCEPTION_INFO);
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            Console.WriteLine(Constants.ERROR_CAN_NOT_FIND);
            return false;
        }
        public void Print()
        {
            if (this.productCategoryDictionary.Count > 0)
            {
                var table = new ConsoleTable("[ID]", "Ten loai hang");
                foreach (ProductCategory productCategory in this.productCategoryDictionary.Values)
                {
                    table.AddRow(productCategory.Id, productCategory.Name);
                }
                table.Write();
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
            else
            {
                Console.WriteLine(Constants.ERROR_NOT_SAVE_ANY_INFO);
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
        }
        public void Find(string name)
        {
            if (this.productCategoryDictionary.Count > 0)
            {
                Colors.WriteLine(Constants.FOUND_PRODUCTS_CATEGORY_RESULTS.DarkYellow());
                var table = new ConsoleTable("[ID]", "Ten loai hang");
                foreach (ProductCategory productCategory in this.productCategoryDictionary.Values)
                {
                    if (productCategory.Name == name || productCategory.Id == name)
                    {
                        table.AddRow(productCategory.Id, productCategory.Name);
                    }
                }
                table.Write();
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
            else
            {
                Console.WriteLine(Constants.EMPTY_PRODUCTS_CATEGORY);
                return;
            }
        }
        public bool categoryNameExist(string name)
        {
            if (this.productCategoryDictionary.Count > 0)
            {
                foreach (ProductCategory productCategory in this.productCategoryDictionary.Values)
                {
                    if (productCategory.Name == name)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        public void Edit(string name, string value)
        {
            if (this.productCategoryDictionary.Count > 0)
            {
                foreach (ProductCategory productCategory in this.productCategoryDictionary.Values)
                {
                    if (productCategory.Name == name || productCategory.Id == name)
                    {
                        //Only allow to edit name value
                        productCategory.Name = value;
                        Console.WriteLine(Constants.SUCCESSFULL_EDIT);
                        return;
                    }
                }

                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
            else
            {
                Console.WriteLine(Constants.EMPTY_PRODUCTS_CATEGORY);
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
        }
    }


    [Serializable]
    public class Product
    {
        private string id;
        private string name;
        private DateTime manufacturingDate;
        private DateTime expiryDate;
        private string company;
        private string category;
        private bool isExpired;
        private int stock;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public DateTime ManufacturingDate { get => manufacturingDate; set => manufacturingDate = value; }
        public DateTime ExpiryDate { get => expiryDate; set => expiryDate = value; }
        public string Company { get => company; set => company = value; }
        public string Category { get => category; set => category = value; }
        public bool IsExpired { get => isExpired; }
        public int Stock { get => stock; set => stock = value; }
        public IEnumerable<Product> Values { get; internal set; }

        public Product(string id, string name, string manufacturingDate, string company, string category, string expiryDate, int stock)
        {
            this.id = id;
            this.name = name;
            DateTime.TryParse(manufacturingDate, out this.manufacturingDate);
            DateTime.TryParse(expiryDate, out this.expiryDate);
            this.company = company;
            this.category = category;
            this.isExpired = CalIsExpired();
            this.stock = stock;
        }
        public Product()
        {
            this.id = string.Empty;
            this.name = string.Empty;
            this.manufacturingDate = DateTime.Now;
            this.expiryDate = DateTime.Now;
            this.company = string.Empty;
            this.category = string.Empty;
            this.isExpired = true;
            this.stock = 0;
        }
        private string InputField(string displayMessage)
        {
            Console.Write(displayMessage);
            return Console.ReadLine();
        }
        public bool CalIsExpired()
        {
            DateTime now = DateTime.Now;
            TimeSpan diff = this.expiryDate - now;
            int daysLeft = diff.Days;
            if (daysLeft <= 0)
            {
                return true;
            }
            return false;
        }
        public void InputProductInfo()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(Constants.INPUT_PRODUCT_MESSAGE);
            this.name = InputField(Constants.INPUT_PRODUCT_NAME_MESSAGE);
            DateTime.TryParse(InputField(Constants.INPUT_MANUFACTURING_DATE_MESSAGE), out this.manufacturingDate);
            DateTime.TryParse(InputField(Constants.INPUT_EXPIRY_DATE_MESSAGE), out this.expiryDate);
            this.company = InputField(Constants.INPUT_COMPANY_MESSAGE);
            this.category = InputField(Constants.INPUT_CATEGORY_MESSAGE);
            this.isExpired = CalIsExpired();
            this.stock = int.Parse(InputField(Constants.INPUT_STOCK_MESSAGE));
            Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
        }
    }
    class ProductInformation
    {
        private static ProductInformation productInformation;
        private Dictionary<string, Product> productDictionary;
        private readonly BinaryFormatter formatter;
        private const string DATA_FILENAME = "products.dat";
        public static ProductInformation Instance()
        {
            if (productInformation == null)
            {
                productInformation = new ProductInformation();
            }
            return productInformation;
        }
        private ProductInformation()
        {
            this.productDictionary = new Dictionary<string, Product>();
            this.formatter = new BinaryFormatter();
        }
        public bool Add(string id, string name, string manufacturingDate, string company, string category, string expiryDate, int stock)
        {
            if (this.productDictionary.ContainsKey(name))
            {
                Console.WriteLine(String.Format(Constants.ERROR_ALREADY_SAVE, name));
                return false;
            }
            else
            {
                this.productDictionary.Add(name, new Product(Guid.NewGuid().ToString(), name, manufacturingDate, company, category, expiryDate, stock));
                ProductCategoryInformation productCategory = ProductCategoryInformation.Instance();
                productCategory.Load();
                if (!productCategory.categoryNameExist(category))
                {
                    productCategory.Add(category);
                }
                Console.WriteLine(Constants.SUCCESSFULL_ADD);
                return true;
            }

        }
        public bool Remove(string name)
        {
            if (!this.productDictionary.ContainsKey(name))
            {
                Console.WriteLine(String.Format(Constants.ERROR_NOT_SAVE_ANY_INFO));
                return false;
            }
            if (this.productDictionary.Remove(name))
            {
                Console.WriteLine(String.Format(Constants.SUCCESSFULL_REMOVE, name));
                return true;
            }
            else
            {
                Console.WriteLine(String.Format(Constants.ERROR_CAN_NOT_REMOVE, name));
                return false;
            }
        }
        public bool Save()
        {
            try
            {
                FileStream writerFileStream = new FileStream(DATA_FILENAME, FileMode.Create, FileAccess.Write);
                this.formatter.Serialize(writerFileStream, this.productDictionary);
                writerFileStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ERROR_CAN_NOT_SAVE);
                Console.WriteLine(ex.Message);
                return true;
            }
        }
        public bool Load()
        {
            if (File.Exists(DATA_FILENAME))
            {
                try
                {
                    FileStream readerFileStream = new FileStream(DATA_FILENAME, FileMode.Open, FileAccess.Read);
                    productDictionary = (Dictionary<String, Product>)formatter.Deserialize(readerFileStream);
                    readerFileStream.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(Constants.EXCEPTION_INFO);
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            Console.WriteLine(Constants.ERROR_CAN_NOT_FIND);
            return false;
        }
        public void Print()
        {
            if (this.productDictionary.Count > 0)
            {
                var table = new ConsoleTable("[ID]", "[Ten san pham]", "[Ngay san xuat]", "Ngay het han" ,"[Cong ty]", "[Loai hang]", "Ton kho");
                foreach (Product product in this.productDictionary.Values)
                {
                    table.AddRow(product.Id, product.Name, product.ManufacturingDate, product.ExpiryDate , product.Company, product.Category, product.Stock);
                }
                table.Write();
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
            else
            {
                Console.WriteLine(Constants.ERROR_NOT_SAVE_ANY_INFO);
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
        }
        public void Find(string name)
        {
            if (this.productDictionary.Count > 0)
            {
                Colors.WriteLine(Constants.FOUND_PRODUCTS_RESULTS.DarkYellow());
                var table = new ConsoleTable("[ID]", "[Ten san pham]", "[Ngay san xuat]", "Ngay het han", "[Cong ty]", "[Loai hang]", "Ton kho");
                foreach (Product product in this.productDictionary.Values)
                {
                    if (product.Name == name || product.Id == name)
                    {
                        table.AddRow(product.Id, product.Name, product.ManufacturingDate, product.ExpiryDate, product.Company, product.Category, product.Stock);
                    }
                }
                table.Write();
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
            else
            {
                Console.WriteLine(Constants.EMPTY_PRODUCTS);
            }
        }
        public bool FindByCategory(string cat)
        {
            if (this.productDictionary.Count > 0)
            {
                foreach (Product product in this.productDictionary.Values)
                {
                    if (product.Category == cat)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
   
        }
        public void Edit(string name, string key, string value)
        {
            var correspondValue = this.productDictionary[key];
            if (correspondValue == null)
            {
                Console.WriteLine(Constants.PROPERTY_NOT_FOUND);
                return;
            }
            if (this.productDictionary.Count > 0)
            {
                foreach (Product product in this.productDictionary.Values)
                {
                    if (product.Name == name || product.Id == name)
                    {
                        Console.WriteLine(Constants.SUCCESSFULL_EDIT);

                        Console.WriteLine(String.Format("{0} | {1} | {2:d} | {3} | {4} | {5} | {6:d} | {7}", product.Id, product.Name, product.ManufacturingDate, product.Company, product.Category, product.IsExpired, product.ExpiryDate, product.Stock));
                    }
                }

                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
            else
            {
                Console.WriteLine(Constants.EMPTY_PRODUCTS);
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
        }
        public void FindExpiredProducts()
        {
            if (this.productDictionary.Count > 0)
            {
                Console.WriteLine(Constants.FOUND_PRODUCTS_RESULTS);
                foreach (Product product in this.productDictionary.Values)
                {
                    if (product.IsExpired)
                    {
                        Console.WriteLine(String.Format("{0} | {1} | {2:d} | {3} | {4} | {5} | {6:d} | {7}", product.Id, product.Name, product.ManufacturingDate, product.Company, product.Category, product.IsExpired, product.ExpiryDate, product.Stock));
                    }
                }
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
            else
            {
                Console.WriteLine(Constants.EMPTY_PRODUCTS);
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
        }
        public void ListProductsStockByCategory(string category)
        {
            if (this.productDictionary.Count > 0)
            {
                int count = 0;
                foreach (Product product in this.productDictionary.Values)
                {
                    if (product.Category == category)
                    {
                        count += product.Stock;
                    }
                }
                Console.WriteLine(Constants.STOCK_BY_CATEGORY, count, category);
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
            else
            {
                Console.WriteLine(Constants.EMPTY_PRODUCTS);
                Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
            }
        }
    }




    class Action
    {
        public enum Options
        {
            CreateProduct = 1, DeleteInfoProduct = 2, EditInfoProduct = 3, FindInfoProduct = 4, ViewInfoProduct = 5, CreateProductCategory = 6, DeleteInfoProductCategory = 7, EditInfoProductCategory = 8, FindInfoProductCategory = 9, ViewInfoProductCategory = 10, ListProductsStockByCategory = 11, ListExpiredProducts = 12, Exit = 13, ClearConsole = 14
        };
        Product product = new Product();
        private ProductCategory productCat;
        public void Menu()
        {
            Console.Clear();
            ProductInformation fi = ProductInformation.Instance();
            ProductCategoryInformation pci = ProductCategoryInformation.Instance();
            Options option;
            fi.Load();
            pci.Load();
            do
            {
                Colors.WriteLine(Constants.MENU_START.OnDarkCyan());
                Colors.WriteLine(Constants.MENU_CREATE_PRODUCT.DarkBlue());
                Colors.WriteLine(Constants.MENU_DELETE_PRODUCT_INFO.DarkBlue());
                Colors.WriteLine(Constants.MENU_EDIT_PRODUCT_INFO.DarkBlue());
                Colors.WriteLine(Constants.MENU_FIND_PRODUCT_INFO.DarkBlue());
                Colors.WriteLine(Constants.MENU_VIEW_PRODUCT_INFO.DarkBlue());
                Colors.WriteLine(Constants.MENU_CREATE_PRODUCT_CATEGORY.DarkBlue());
                Colors.WriteLine(Constants.MENU_DELETE_PRODUCT_CATEGORY_INFO.DarkBlue());
                Colors.WriteLine(Constants.MENU_EDIT_PRODUCT_CATEGORY_INFO.DarkBlue());
                Colors.WriteLine(Constants.MENU_FIND_PRODUCT_CATEGORY_INFO.DarkBlue());
                Colors.WriteLine(Constants.MENU_VIEW_PRODUCT_CATEGORY_INFO.DarkBlue());
                Colors.WriteLine(Constants.MENU_LIST_PRODUCTS_STOCK_BY_CATEGORY.DarkBlue());
                Colors.WriteLine(Constants.MENU_LIST_EXPIRED_PRODUCTS.DarkBlue());
                Colors.WriteLine(Constants.MENU_EXIT.DarkBlue());
                Colors.WriteLine(Constants.MENU_CLEAR_CONSOLE.DarkBlue());
                Console.WriteLine();
                Console.Write(">> ");
                Enum.TryParse(Console.ReadLine(), out option);
                switch (option)
                {
                    //product
                    case Options.CreateProduct:
                        product = product ?? new Product();
                        product.InputProductInfo();
                        if (fi.Add(Guid.NewGuid().ToString(), product.Name, product.ManufacturingDate.ToString(), product.Company, product.Category, product.ExpiryDate.ToString(), product.Stock))
                        {
                            fi.Save();
                        }
                        break;
                    case Options.ViewInfoProduct:
                        fi.Print();
                        break;
                    case Options.DeleteInfoProduct:
                        Console.WriteLine(Constants.INPUT_DELETE_PRODUCT_NAME);
                        fi.Remove(Console.ReadLine());
                        fi.Save();
                        break;
                    case Options.EditInfoProduct:
                        Console.WriteLine(Constants.INPUT_EDIT_PRODUCT_NAME);
                        string name = Console.ReadLine();
                        fi.Find(name);
                        Console.WriteLine(Constants.INPUT_EDIT_PRODUCT_PROPERTY);
                        string key = Console.ReadLine();
                        Console.WriteLine(Constants.INPUT_EDIT_VALUE, key);
                        string value = Console.ReadLine();
                        fi.Edit(name, key, value);

                        break;
                    case Options.FindInfoProduct:
                        Console.WriteLine(Constants.INPUT_FIND_PRODUCT_NAME);
                        fi.Find(Console.ReadLine());
                        Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE);
                        break;
                    //product category
                    case Options.CreateProductCategory:
                        productCat = productCat ?? new ProductCategory();
                        productCat.InputProductCategoryInfo();
                        if (pci.Add(productCat.Name))
                        {
                            pci.Save();
                        }
                        break;
                    case Options.ViewInfoProductCategory:
                        pci.Print();
                        break;
                    case Options.DeleteInfoProductCategory:
                        Console.WriteLine(Constants.INPUT_DELETE_PRODUCT_CATEGORY_NAME);
                        string catName = Console.ReadLine();
                        pci.Remove(catName);
                        pci.Save();
                        break;
                    case Options.EditInfoProductCategory:
                        Console.WriteLine(Constants.INPUT_EDIT_PRODUCT_CATEGORY_NAME);
                        string edittedCatName = Console.ReadLine();
                        pci.Find(edittedCatName);
                        Console.WriteLine(Constants.INPUT_EDIT_VALUE, edittedCatName);
                        string updatedCatValue= Console.ReadLine();
                        pci.Edit(edittedCatName, updatedCatValue);
                        Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
                        break;
                    case Options.FindInfoProductCategory:
                        Console.WriteLine(Constants.INPUT_FIND_PRODUCT_CATEGORY_NAME);
                        pci.Find(Console.ReadLine());

                        Colors.WriteLine(Constants.END_OF_PAGE_MESSAGE.DarkRed());
                        break;
                    case Options.ListExpiredProducts:
                        fi.FindExpiredProducts();
                        break;
                    case Options.ListProductsStockByCategory:
                        Console.WriteLine(Constants.INPUT_FIND_PRODUCT_BY_CATEGORY);
                        fi.ListProductsStockByCategory(Console.ReadLine());
                        break;
                    case Options.Exit:
                        Console.WriteLine(Constants.MENU_GOODBYE);
                        break;
                    case Options.ClearConsole:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine(Constants.MENU_NOT_SUPPORT);
                        break;
                }
                Console.ReadKey();

            } while (option != Options.Exit);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create object Action.
            Action action = new Action();
            // Screen for user choice action
            action.Menu();
        }
    }
}
