using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        public const string MENU_START = "Vui long chon nhung lua chon sau day:";
        public const string MENU_CREATE_PRODUCT = "1: Tao moi hoac them san pham moi";
        public const string MENU_DELETE_PRODUCT_INFO = "2: Xoa san pham";
        public const string MENU_EDIT_PRODUCT_INFO = "3: Sua thong tin san pham";
        public const string MENU_FIND_PRODUCT_INFO = "4: Tim san pham theo ten";
        public const string MENU_VIEW_PRODUCT_INFO = "5: Xem tat ca san pham";
        public const string MENU_LIST_PRODUCTS_STOCK_BY_CATEGORY = "19: Liet ke hang ton kho theo loai hang";
        public const string MENU_LIST_EXPIRED_PRODUCTS = "20: Liet ke hang het han su dung";
        public const string MENU_EXIT = "21: Thoat chuong trinh";
        public const string MENU_GOODBYE = "Tam biet!";
        public const string MENU_NOT_SUPPORT = "Lua chon nay chua duoc ho tro, xin vui long chon lai";

        public const string INPUT_MESSAGE = "Nhap thong tin san pham: ";
        public const string INPUT_NAME_MESSAGE = "Nhap ten san pham: ";
        public const string INPUT_EXPIRY_DATE_MESSAGE = "Nhap han su dung: ";
        public const string INPUT_MANUFACTURING_DATE_MESSAGE = "Nhap ngay san xuat: ";
        public const string INPUT_COMPANY_MESSAGE = "Nhap cong ty san xuat: ";
        public const string INPUT_CATEGORY_MESSAGE = "Nhap mat hang: ";
        public const string INPUT_STOCK_MESSAGE = "Nhap so luong ton: ";
        public const string INPUT_DELETE_PRODUCT_NAME = "Nhap ten san pham ban muon xoa: ";
        public const string INPUT_FIND_PRODUCT_NAME = "Nhap ten hoac id cua san pham ban muon tim kiem: ";
        public const string INPUT_EDIT_PRODUCT_NAME = "Nhap ten hoac id cua san pham ban muon cap nhat: ";
        public const string INPUT_EDIT_PRODUCT_PROPERTY = "Nhap thuoc tinh ma ban muon cap nhat: ";
        public const string INPUT_EDIT_VALUE = "Nhap gia tri moi cua {0}: ";
        public const string INPUT_FIND_PRODUCT_BY_CATEGORY = "Nhap ten loai hang can kiem tra ton kho, ten loai hang gom co: ";

        public const string END_OF_PAGE_MESSAGE = "==== NHAN MOT PHIM BAT KY DE TIEP TUC ===";
        public const string EMPTY_PRODUCTS = "Hien khong co san pham nao";
        public const string FOUND_PRODUCTS_RESULTS = "==== Tim duoc nhung san pham co ten hoac id sau: ====";
        public const string STOCK_BY_CATEGORY = "Con lai {0} san pham thuoc mat hang {1} trong kho!";

        public const string ERROR_NOT_SAVE_ANY_INFO = "Khong co ket qua nao";
        public const string ERROR_CAN_NOT_FIND = "cant find traiee info file";
        public const string ERROR_CAN_NOT_SAVE = "cant save traiee info file";
        public const string ERROR_ALREADY_SAVE = "you had already added {0} before";
        public const string ERROR_CAN_NOT_FILE_INFO = "{0} chua duoc them truoc day";
        public const string ERROR_CAN_NOT_REMOVE = "Khong the xoa {0}";
        public const string PROPERTY_NOT_FOUND = "Thuoc tinh nay khong ton tai!";

        public const string SUCCESSFULL_ADD = "Them thanh cong!";
        public const string SUCCESSFULL_REMOVE = "Xoa thanh cong!";
        public const string SUCCESSFULL_EDIT = "Cap nhat thanh cong!";
        public const string EXCEPTION_INFO = "exception info";
        public const string HEAD_PRODUCT_INFO = "[ID] [ten san pham] [ngay san xuat] [cong ty san xuat] [loai hang] [han su dung]";
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
            this.stock= 0;
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
            Console.WriteLine(Constants.INPUT_MESSAGE);
            this.name = InputField(Constants.INPUT_NAME_MESSAGE);
            DateTime.TryParse(InputField(Constants.INPUT_MANUFACTURING_DATE_MESSAGE), out this.manufacturingDate);
            DateTime.TryParse(InputField(Constants.INPUT_EXPIRY_DATE_MESSAGE), out this.expiryDate);
            this.company = InputField(Constants.INPUT_COMPANY_MESSAGE);
            this.category = InputField(Constants.INPUT_CATEGORY_MESSAGE);
            this.isExpired = CalIsExpired();
            this.stock = int.Parse(InputField(Constants.INPUT_STOCK_MESSAGE));
            Console.WriteLine(Constants.END_OF_PAGE_MESSAGE);
        }

        public static implicit operator Product(string v)
        {
            throw new NotImplementedException();
        }
    }
    class ProductInformation
    {
        private static ProductInformation productInformation;
        private Dictionary<string, Product> productDictionary;
        private readonly BinaryFormatter formatter;
        private const string DATA_FILENAME = "productsinformation.dat";
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
                Console.WriteLine(Constants.HEAD_PRODUCT_INFO);
                foreach (Product product in this.productDictionary.Values)
                {
                    Console.WriteLine(String.Format("{0} | {1} | {2:d} | {3} | {4} | {5} | {6:d} | {7}", product.Id, product.Name, product.ManufacturingDate, product.Company, product.Category, product.IsExpired, product.ExpiryDate, product.Stock));
                }
                Console.WriteLine(Constants.END_OF_PAGE_MESSAGE);
            }
            else
            {
                Console.WriteLine(Constants.ERROR_NOT_SAVE_ANY_INFO);
                Console.WriteLine(Constants.END_OF_PAGE_MESSAGE);
            }
        }
        public void Find(string name)
        {
            if (this.productDictionary.Count > 0)
            {
                foreach (Product product in this.productDictionary.Values)
                {
                    if (product.Name == name || product.Id == name)
                    {
                        Console.WriteLine(Constants.FOUND_PRODUCTS_RESULTS);
                        Console.WriteLine(String.Format("{0} | {1} | {2:d} | {3} | {4} | {5} | {6:d} | {7}", product.Id, product.Name, product.ManufacturingDate, product.Company, product.Category, product.IsExpired, product.ExpiryDate, product.Stock));
                    }
                }
                Console.WriteLine(Constants.END_OF_PAGE_MESSAGE);
            }
            else
            {
                Console.WriteLine(Constants.EMPTY_PRODUCTS);
            }
        }
        public void Edit(string name, string key, string value)
        {
            //int count = 0;
            var correspondValue = this.productDictionary[key];
            if (correspondValue == null)
            {
                Console.WriteLine(Constants.PROPERTY_NOT_FOUND);
                //Console.WriteLine("Vui long nhap lai ten thuoc tinh");
                //Edit(name, Console.ReadLine(), value);
                return;
            }
            if (this.productDictionary.Count > 0)
            {
                foreach (Product product in this.productDictionary.Values)
                {
                    if (product.Name == name || product.Id == name)
                    {
                        Console.WriteLine(Constants.SUCCESSFULL_EDIT);
                        //System.Type typeOfValue = product[key].GetType();
                        this.productDictionary[key] = value;

                        Console.WriteLine(String.Format("{0} | {1} | {2:d} | {3} | {4} | {5} | {6:d} | {7}", product.Id, product.Name, product.ManufacturingDate, product.Company, product.Category, product.IsExpired, product.ExpiryDate, product.Stock));
                    }
                }

                Console.WriteLine(Constants.END_OF_PAGE_MESSAGE);
            }
            else
            {
                Console.WriteLine(Constants.EMPTY_PRODUCTS);
                Console.WriteLine(Constants.END_OF_PAGE_MESSAGE);
            }
        }
        public void FindExpiredProducts()
        {
            if (this.productDictionary.Count > 0)
            {
                foreach (Product product in this.productDictionary.Values)
                {
                    if (product.IsExpired)
                    {
                        Console.WriteLine(Constants.FOUND_PRODUCTS_RESULTS);
                        Console.WriteLine(String.Format("{0} | {1} | {2:d} | {3} | {4} | {5} | {6:d} | {7}", product.Id, product.Name, product.ManufacturingDate, product.Company, product.Category, product.IsExpired, product.ExpiryDate, product.Stock));
                    }
                }
                Console.WriteLine(Constants.END_OF_PAGE_MESSAGE);
            }
            else
            {
                Console.WriteLine(Constants.EMPTY_PRODUCTS);
                Console.WriteLine(Constants.END_OF_PAGE_MESSAGE);
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
                Console.WriteLine(Constants.END_OF_PAGE_MESSAGE);
            }
            else
            {
                Console.WriteLine(Constants.EMPTY_PRODUCTS);
                Console.WriteLine(Constants.END_OF_PAGE_MESSAGE);
            }
        }
    }




    class Action
    {
        public enum Options
        {
            CreateProduct = 1, DeleteInfoProduct = 2, EditInfoProduct = 3, FindInfoProduct = 4, ViewInfoProduct = 5, ListProductsStockByCategory = 19, ListExpiredProducts = 20, Exit = 21
        };
        private Product product;
        public void Menu()
        {
            ProductInformation fi = ProductInformation.Instance();
            Options option;
            fi.Load();
            do
            {
                Console.Clear();
                Console.WriteLine(Constants.MENU_START);
                Console.WriteLine(Constants.MENU_CREATE_PRODUCT);
                Console.WriteLine(Constants.MENU_DELETE_PRODUCT_INFO);
                Console.WriteLine(Constants.MENU_EDIT_PRODUCT_INFO);
                Console.WriteLine(Constants.MENU_FIND_PRODUCT_INFO);
                Console.WriteLine(Constants.MENU_VIEW_PRODUCT_INFO);
                Console.WriteLine(Constants.MENU_LIST_PRODUCTS_STOCK_BY_CATEGORY);
                Console.WriteLine(Constants.MENU_LIST_EXPIRED_PRODUCTS);
                Console.WriteLine(Constants.MENU_EXIT);
                Enum.TryParse(Console.ReadLine(), out option);
                switch (option)
                {
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
                        Console.WriteLine(Constants.END_OF_PAGE_MESSAGE);
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
