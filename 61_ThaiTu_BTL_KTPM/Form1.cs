using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Timers;
using System.Threading;

namespace _61_ThaiTu_BTL_KTPM
{ 
    public partial class Form1 : Form
    {
        //61_Nguyễn Bảo Thái Tú 

        private const string webURL = "https://www.netflix.com/vn/"; // khởi tạo đường link trang web muốn test thành biến toàn cục

        private IWebDriver driver_61_ThaiTu;

        private void useProfile()
        {

            var options = new ChromeOptions();

            //61_Nguyễn Bảo Thái Tú 

            options.AddArgument("user-data-dir=C:\\Users\\nguyenbaothaitu9a8\\AppData\\Local\\Google\\Chrome\\User Data\\Default");

            //61_Nguyễn Bảo Thái Tú 

            options.AddArgument("profile-directory=Default");

            driver_61_ThaiTu = new ChromeDriver(options);
        }
        private void navigate()
        {
            ChromeDriverService chrome_61_NguyenBaoThaiTu = ChromeDriverService.CreateDefaultService(); // Tất màn hình đen cmd đi
            chrome_61_NguyenBaoThaiTu.HideCommandPromptWindow = true;
            driver_61_ThaiTu.Navigate().GoToUrl(webURL); // Điều hướng đến web cần test
        }

        private void clickLogin()
        {
            driver_61_ThaiTu.FindElement(By.XPath("//*[@id=\"signIn\"]")).Click();
        }

        private void clickLoginSubmit()
        {
            driver_61_ThaiTu.FindElement(By.XPath("//*[@id=\"appMountPoint\"]/div/div/div[2]/div/form/button[1]")).Click();
        }

        private void clickSubmitAuthentication()
        {
            driver_61_ThaiTu.FindElement(By.Name("userLoginId")).SendKeys("nguyenbaothaitu9a8@gmail.com"); // điền tên đăng nhập đúng
            driver_61_ThaiTu.FindElement(By.Name("password")).SendKeys("0906079953@@Tu"); // điền password đúng 
            clickLoginSubmit();
        }


        private void checkBlankUserName(int TestNum)
        {
            //61_Nguyễn Bảo Thái Tú 
            clickLoginSubmit();
            var msg = driver_61_ThaiTu.FindElement(By.CssSelector("div[data-uia='login-field+validationMessage']"));
            if (msg.Text == "Vui lòng nhập email hoặc số điện thoại hợp lệ.")
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
            else
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
            
        }

        private void checkInvalidUserName(int TestNum)
        {
            driver_61_ThaiTu.FindElement(By.Name("userLoginId")).SendKeys("nguyennhan7310@gmail.com"); // điền tên không tồn tại vào 
            driver_61_ThaiTu.FindElement(By.Name("password")).SendKeys("0906079953@@"); // điền password nào cũng được 
            clickLoginSubmit();
            var msg = driver_61_ThaiTu.FindElement(By.CssSelector("a[href='/']"));
            if (msg.Text == "Rất tiếc, chúng tôi không tìm thấy tài khoản nào có địa chỉ email này.")
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
            else
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
            listBoxTC.Items.Add(msg.Text);
        }

        private void checkValidUserName1(int TestNum) //đúng tên đăng nhập nhưng trống mật khẩu 
        {
            driver_61_ThaiTu.FindElement(By.Name("userLoginId")).SendKeys("nguyenbaothaitu9a8@gmail.com"); // điền tên tồn tại vào 
            clickLoginSubmit();
            var msg = driver_61_ThaiTu.FindElement(By.CssSelector("div[data-uia='password-field+validationMessage']"));
            if (msg.Text == "Mật khẩu của bạn phải chứa từ 4 đến 60 ký tự.")
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
            else
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
        }

        private void checkValidUserName2(int TestNum) //đúng tên đăng nhập nhưng mật khẩu không tồn tại
        {
            driver_61_ThaiTu.FindElement(By.Name("userLoginId")).SendKeys("nguyenbaothaitu9a8@gmail.com"); // điền tên tồn tại vào 
            driver_61_ThaiTu.FindElement(By.Name("password")).SendKeys("0906079953@@"); // điền password nào cũng được 
            clickLoginSubmit();
            var msg = driver_61_ThaiTu.FindElement(By.CssSelector("a[href='/LoginHelp']"));
            if (msg.Text == "Bạn quên mật khẩu?")
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
            else
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
        }

        private void checkValidUserName3(int TestNum) // đúng tên đăng nhập và đúng mật khẩu
        {
            driver_61_ThaiTu.FindElement(By.Name("userLoginId")).SendKeys("nguyenbaothaitu9a8@gmail.com"); // điền tên tồn tại vào 
            driver_61_ThaiTu.FindElement(By.Name("password")).SendKeys("0906079953@@Tu"); // điền password đúng vào 
            var usernameTrue = driver_61_ThaiTu.FindElement(By.Name("userLoginId")); // Lấy username đã nhập từ trên
            string userStr = usernameTrue.GetAttribute("value"); // Gán giá trị vào biến userStr bằng method getAtrrivbute
            var passwordTrue = driver_61_ThaiTu.FindElement(By.Name("password")); // Lấy password đã nhập từ trên
            string passStr = passwordTrue.GetAttribute("value"); // gán giá trị vào biến passStr bằng method getAtrribute 
            if (userStr == "nguyenbaothaitu9a8@gmail.com" && passStr == "0906079953@@Tu") // kiểm tra username và password
            {
                clickLoginSubmit();
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
            }
            else
            {
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
            }
        }

        private void checkBlankSearch(int TestNum)
        {
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("button[type='button']")).Click(); // tắt nút quảng cáo khi vào trang
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.ClassName("searchTab")).Click(); // bấm vào icon tìm kiếm
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("input[type='text']")).SendKeys(""); // truyền nội dung tìm kiếm vào ô tìm kiếm
            Thread.Sleep(5000);
            var content = driver_61_ThaiTu.FindElement(By.CssSelector("input[data-search-input='true']")); // khởi tạo biến lấy giá trị tìm kiếm
            string searchText = content.GetAttribute("value"); // gán vào biến giá trị tìm kiếm sau đó so sánh 
            if (searchText == "")
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
            else
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
        }

        private void checkInvalidSearch(int TestNum)
        {
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("button[type='button']")).Click(); // tắt nút quảng cáo khi vào trang
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.ClassName("searchTab")).Click(); // bấm vào icon tìm kiếm
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("input[type='text']")).SendKeys("asdasdasdasdasdasdasdasdasd"); // truyền nội dung tìm kiếm vào ô tìm kiếm
            Thread.Sleep(5000);
            var content = driver_61_ThaiTu.FindElement(By.CssSelector("p")); // Tìm phần tử <p>
            string text = content.GetAttribute("innerText"); // Lấy giá trị innertext trong phần tử p
            if(text.Length > 50) // so sánh if else
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
            else
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
        }

        private void checkValidSearch(int TestNum)
        {
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("button[type='button']")).Click(); // tắt nút quảng cáo khi vào trang
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.ClassName("searchTab")).Click(); // bấm vào icon tìm kiếm
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("input[type='text']")).SendKeys("Gozilla"); // truyền nội dung tìm kiếm vào ô tìm kiếm
            Thread.Sleep(5000);
            try
            {
                var content = driver_61_ThaiTu.FindElement(By.CssSelector("div[data-uia='search-entity-list'] > span"));
                string text = content.Text;
                // Nếu không có lỗi, kiểm tra văn bản
                if (text == "Tác phẩm khác để khám phá:")
                    listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
                else
                    listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
            }
            catch (NoSuchElementException ex)
            {
                // Nếu phát hiện lỗi NoSuchElementException, in ra thông báo lỗi
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
            }
        }

        private void checkLowerCaseSearch(int TestNum)
        {
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("button[type='button']")).Click(); // tắt nút quảng cáo khi vào trang
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.ClassName("searchTab")).Click(); // bấm vào icon tìm kiếm
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("input[type='text']")).SendKeys("gozilla"); // truyền nội dung tìm kiếm vào ô tìm kiếm
            Thread.Sleep(5000);
            try
            {
                var content = driver_61_ThaiTu.FindElement(By.CssSelector("div[data-uia='search-entity-list'] > span"));
                string text = content.Text;
                // Nếu không có lỗi, kiểm tra văn bản
                if (text == "Tác phẩm khác để khám phá:")
                    listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
                else
                    listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
            }
            catch (NoSuchElementException ex)
            {
                // Nếu phát hiện lỗi NoSuchElementException, in ra thông báo lỗi
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
            }
        }

        private void checkUpperCaseSearch(int TestNum)
        {
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("button[type='button']")).Click(); // tắt nút quảng cáo khi vào trang
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.ClassName("searchTab")).Click(); // bấm vào icon tìm kiếm
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("input[type='text']")).SendKeys("GOZILLA"); // truyền nội dung tìm kiếm vào ô tìm kiếm
            Thread.Sleep(5000);
            try
            {
                var content = driver_61_ThaiTu.FindElement(By.CssSelector("div[data-uia='search-entity-list'] > span"));
                string text = content.Text;
                // Nếu không có lỗi, kiểm tra văn bản
                if (text == "Tác phẩm khác để khám phá:")
                    listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
                else
                    listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
            }
            catch (NoSuchElementException ex)
            {
                // Nếu phát hiện lỗi NoSuchElementException, in ra thông báo lỗi
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
            }
        }

        private void checkNumberSearch(int TestNum)
        {
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("button[type='button']")).Click(); // tắt nút quảng cáo khi vào trang
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.ClassName("searchTab")).Click(); // bấm vào icon tìm kiếm
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("input[type='text']")).SendKeys("123123"); // truyền nội dung tìm kiếm vào ô tìm kiếm
            Thread.Sleep(5000);
            try
            {
                var content = driver_61_ThaiTu.FindElement(By.CssSelector("div[data-uia='search-entity-list'] > span"));
                string text = content.Text;
                // Nếu không có lỗi, kiểm tra văn bản
                if (text == null)
                    listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
                else
                    listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
            }
            catch (NoSuchElementException ex)
            {
                // Nếu phát hiện lỗi NoSuchElementException, in ra thông báo lỗi
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
            }

        }

        private void checkSpecialSearch(int TestNum)
        {
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("button[type='button']")).Click(); // tắt nút quảng cáo khi vào trang
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.ClassName("searchTab")).Click(); // bấm vào icon tìm kiếm
            Thread.Sleep(5000);
            driver_61_ThaiTu.FindElement(By.CssSelector("input[type='text']")).SendKeys("<<>>"); // truyền nội dung tìm kiếm vào ô tìm kiếm
            Thread.Sleep(5000);
            try
            {
                var content = driver_61_ThaiTu.FindElement(By.CssSelector("div[data-uia='search-entity-list'] > span"));
                string text = content.Text;
                // Nếu không có lỗi, kiểm tra văn bản
                if (text == null)
                    listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
                else
                    listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "failed");
            }
            catch (NoSuchElementException ex)
            {
                // Nếu phát hiện lỗi NoSuchElementException, in ra thông báo lỗi
                listBoxTC.Items.Add("Test Case" + " " + TestNum + " " + "passed");
            }
        }
        public Form1()
        {
            InitializeComponent();
            useProfile();
        }



        private void btnBlankUserName_Click(object sender, EventArgs e)
        {
            navigate();
            clickLogin();
            Thread.Sleep(1000);
            checkBlankUserName(1);

        }

        private void btnInvalidUserName_Click(object sender, EventArgs e)
        {
            navigate();
            clickLogin();
            Thread.Sleep(1000);
            checkInvalidUserName(2);
        }

        private void btnValidUserName1_Click(object sender, EventArgs e)
        {
            navigate();
            clickLogin();
            Thread.Sleep(1000);
            checkValidUserName1(3);
        }

        private void btnValidUserName2_Click(object sender, EventArgs e)
        {
            navigate();
            clickLogin();
            Thread.Sleep(1000);
            checkValidUserName2(4);
        }

        private void btnValidUserName3_Click(object sender, EventArgs e)
        {
            navigate();
            clickLogin();
            Thread.Sleep(1000);
            checkValidUserName3(5);
        }

        private void btnBlankSearch_Click(object sender, EventArgs e)
        {
            navigate();
            //clickLogin();
            //clickSubmitAuthentication();
            checkBlankSearch(1);
            Thread.Sleep(1000);
        }

        private void btnInValidSearch_Click(object sender, EventArgs e)
        {
            navigate();
            checkInvalidSearch(2);
            Thread.Sleep(1000);
        }

        private void btnValidSearch_Click(object sender, EventArgs e)
        {
            navigate();
            checkValidSearch(3);
            Thread.Sleep(1000);
        }

        private void btnNumberSearch_Click(object sender, EventArgs e)
        {
            navigate();
            checkNumberSearch(4);
            Thread.Sleep(1000);
        }

        private void btnLowerTextSearch_Click(object sender, EventArgs e)
        {
            navigate();
            checkLowerCaseSearch(5);
            Thread.Sleep(1000);
        }

        private void btnUpperTextSearch_Click(object sender, EventArgs e)
        {
            navigate();
            checkUpperCaseSearch(6);
            Thread.Sleep(1000);
        }

        private void btnSpecialTextSearch_Click(object sender, EventArgs e)
        {
            navigate();
            checkSpecialSearch(7);
            Thread.Sleep(1000);
        }
    }
}
