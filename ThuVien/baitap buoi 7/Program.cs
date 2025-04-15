using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace baitap_buoi_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List <Book> books = new List<Book>();
            List<Reader> readers = new List<Reader>();

            bool menu = true;
            while (menu == true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Chuong trinh quan ly thu vien\n-----------------------");
                Console.ResetColor();
                Console.WriteLine("1: Them sach moi");
                Console.WriteLine("2: Them doc gia moi");
                Console.WriteLine("3: Doc gia muon sach");
                Console.WriteLine("4: Hien thi sach va doc gia duoi dang JSON");
                Console.WriteLine("5: Thoat chuong trinh");
                Console.Write("Chon: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        themsachmoi(books);
                        break;
                    case "2":
                        themdocgia(readers);
                        break;
                    case "3":
                        docgiamuonsach(books, readers);
                        break;
                    case "4":
                        hiendanhsach(books, readers);
                        break;
                    case "5":
                        exit();
                        break;
                    default:
                        error("Khong hop le, vui long chon lai !");
                        break;
                }
            }

            #region thoat
            void exit()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Dang thoat.");
                Thread.Sleep(300);
                Console.WriteLine("Dang thoat..");
                Thread.Sleep(300);
                Console.WriteLine("Dang thoat...");
                Thread.Sleep(300);
                Console.Clear();
                menu = false;
            }
            #endregion

        }
        #region chuc nang phu
        static void error(string noidung)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(noidung+"\nAn phim bat ky de quay ve..");
            Console.ReadKey();
        }
        static void susscesfully(string noidung)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(noidung + "\nAn phim bat ky de quay ve..");
            Console.ReadKey();
        }
 

        #endregion

        #region them sach moi
        static void themsachmoi(List<Book> books)
        {
            Console.Clear();
            Console.Write("Nhap id sach: ");
            int IdSach = int.Parse(Console.ReadLine());

            #region
            var kiemtraid = books.FirstOrDefault(x => x.Id == IdSach);
            if (kiemtraid != null)
            {
                error("Id da ton tai, vui long nhap lai !");
                return;
            }
            #endregion

            Console.Write("Nhap tieu de sach: ");
            string TieuDeSach = Console.ReadLine();
            Console.Write("Nhap tac gia sach: ");
            string TacgiaSach = Console.ReadLine();
            Console.Write("Nhap so luong sach: ");
            int SoluongSach = int.Parse(Console.ReadLine());
            var sachthem = new Book()
            {
                Id = IdSach,
                Title = TieuDeSach,
                Author = TacgiaSach,
                Quantity = SoluongSach,
            };
            books.Add(sachthem);
            susscesfully($"Them sach '{TieuDeSach} thanh cong !");
        }
        #endregion

        #region them doc gia moi
        static void themdocgia(List<Reader> readers)
        {
            Console.Clear();
            Console.Write("Nhap id doc gia: ");
            int id = int.Parse(Console.ReadLine());

            #region kiemtra docgia 
            var kiemtraid = readers.FirstOrDefault(x => x.Id == id);
            if (kiemtraid != null)
            {
                error("Id da ton tai, vui long nhap lai !");
                return;
            }
            #endregion

            Console.Write("Nhap ten doc gia: ");
            string ten = Console.ReadLine();
            Console.Write("Nhap dia chi doc gia: ");
            string diachi = Console.ReadLine();
            var docgia = new Reader()
            {
                Id = id,
                Name = ten,
                Address = diachi
            };
            readers.Add(docgia);
            susscesfully($"Them doc gia '{ten} thanh cong !");
        }
        #endregion

        #region them doc gia muon sach
        static void docgiamuonsach(List<Book> books, List<Reader> readers)
        {
            Console.Clear();

            #region kiemtra doc gia
            Console.Write("Nhap id doc gia muon sach: ");
            int iddocgia = int.Parse(Console.ReadLine());
            var kiemtraid = readers.FirstOrDefault(s => s.Id == iddocgia);
            if (kiemtraid == null)
            {
                error($"Doc gia '{iddocgia}' khong ton tai trong he thong !");
                return;
            }
            #endregion

            List<Book> sachmuon = new List<Book>();

            #region nhap so luong sach muon, va sach can muon
            Console.Write("Nhap so luong sach can muon: ");
            int soluong = int.Parse(Console.ReadLine());
            for (int i = 0; i < soluong; i++)
            {
                Console.Clear();

                #region hien thi sach
                Console.Write($"Sach muon :");
                foreach (var o in sachmuon)
                {
                    Console.Write(o.Title + ", ");
                }
                #endregion

                #region kiemtra sach
                Console.Write("\nNhap id sach can muon: ");
                int idsach = int.Parse(Console.ReadLine());
                var kiemtrasach = books.FirstOrDefault(s => s.Id == idsach);
                if (kiemtrasach == null)
                {
                    error($"Sach '{idsach}' khong ton tai trong he thong !");
                    return;
                }
                var kiemtratontai = kiemtraid.Books.FirstOrDefault(s => s.Id == idsach);
                if (kiemtratontai != null)
                {
                    error($"Doc gia'{iddocgia}' da muon sach '{idsach}' nay roi !");
                    return;
                }
                #endregion
                Book sachmoi = new Book()
                {
                    Id = kiemtrasach.Id,
                    Title = kiemtrasach.Title,
                    Author = kiemtrasach.Author,
                    Quantity = kiemtrasach.Quantity - 1
                };
                kiemtraid.Books.Add(sachmoi);
                sachmuon.Add(sachmoi);
                Console.WriteLine($"Da them sach {kiemtrasach.Title}");
            }
            #endregion

            susscesfully($"Them sach da muon cho doc gia '{iddocgia}' thanh cong !");

        }

        #endregion

        static void hiendanhsach(List<Book> books, List<Reader> readers)
        {
            Console.Clear();

            string hienjson = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });

            string hienjson2 = JsonSerializer.Serialize(readers, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine("Danh sach sach\n------------------\n"+ hienjson);
            Console.WriteLine("Danh sach doc gia\n------------------\n" + hienjson2);
            Console.ReadKey();
        }

    }
}
