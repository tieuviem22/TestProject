using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace testProject2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class User
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Message { get; set; }
        }

        List <User> users = new List <User> () { 
            new User { Email = "dangtung789.td@gmail.com", Password = "123456", Message = null},
            new User { Email = "dangtung789.td@gmail.com", Password = "111111", Message = "Sai mật khẩu"},
            new User { Email = "a@a.com", Password = "abc", Message = "Email không tồn tại"},
        };
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestLogin();
        }

        void TestProject()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            ChromeDriver driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            void uploadFile(string message)
            {
                
                string[] errUrl = { message};
                File.AppendAllLines("data//out.txt", errUrl);
            }

            uploadFile("Test Project :");
            try
            {
                driver.Url = "http://localhost:8080/login";
                driver.Navigate();
                Delay(2);
                

                driver.FindElement(By.CssSelector("input:first-child")).SendKeys("dangtung789.td@gmail.com");
                Delay(2);

                driver.FindElement(By.CssSelector("input[type='password']")).SendKeys("123456");
                Delay(2);

                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                Delay(2);


                //MessageBox.Show("");

                //check project
                //var cards = driver.FindElements(By.ClassName("card"));
                //if(cards.Count == 0)
                //{
                //    uploadFile("Not find a project");
                //    driver.Close();
                //    driver.Quit();
                //}

                //check button create project

                try
                {
                    driver.Url = "http://localhost:8080/projects";
                    Delay(2);
                    driver.FindElement(By.CssSelector("button[class='btn btn-primary btn-lg btn-floating ripple-surface']")).Click();
                    Delay(2);

                    driver.FindElement(By.Name("nameProject")).SendKeys("Project 1");
                    Delay(2);

                    driver.FindElement(By.Name("descriptionProject")).SendKeys("Description 1");
                    Delay(2);

                    driver.FindElement(By.Name("submitFormCreateProject")).Click();
                    Delay(2);


                    if (!driver.FindElement(By.TagName("body")).Text.ToString().Contains("Project 1") || !driver.FindElement(By.TagName("body")).Text.ToString().Contains("Description 1"))
                    {
                        uploadFile("{ Not find new project ( Error create new project) }");
                    }
                    else
                    {
                        uploadFile("{ Create new project success }");
                    }
                }
                catch (Exception ex)
            {
                uploadFile("{ Fail create new project }");
            }




            //check project and button view more project change name project
            try
                {
                    driver.Url = "http://localhost:8080/projects";
                    Delay(2);

                    if (driver.FindElement(By.ClassName("card")) == null)
                    {
                        uploadFile("Không tìm thấy project nào");
                    }


                    var btnMore = driver.FindElements(By.TagName("a"));
                    btnMore[btnMore.Count - 1].Click();

                    if (!driver.FindElement(By.ClassName("popover-body")).Text.ToString().Contains("Rename") || !driver.FindElement(By.ClassName("popover-body")).Text.ToString().Contains("Archive or delete"))
                    {
                        uploadFile("{ Click more project failed }");
                    }
                    driver.FindElement(By.CssSelector("i[class='fas fa-edit']")).Click();
                    Delay(2);

                    if (!driver.FindElement(By.ClassName("modal-content")).Text.ToString().Contains("SAVE CHANGES"))
                    {
                        uploadFile("{ Click Rename failed }");
                    }

                    string nameRd = RandomString(4);
                    string descriptionRd = RandomString(7);
                    driver.FindElement(By.Name("nameProject")).SendKeys(nameRd);
                    Delay(2);
                    driver.FindElement(By.Name("descriptionProject")).SendKeys(descriptionRd);

                    Delay(2);

                    driver.FindElement(By.CssSelector("button[class='btn btn-primary ripple-surface']")).Click();

                    Delay(3);


                    if (driver.FindElement(By.TagName("body")).Text.ToString().IndexOf(nameRd) == -1 || driver.FindElement(By.TagName("body")).Text.ToString().IndexOf(descriptionRd) == -1)
                    {
                        uploadFile("{ Đổi tên nhưng block ra màn hình ko như yêu cầu }");
                    }
                    else
                    {
                        uploadFile("{ Rename project success }");
                    }

                }
                catch
                {
                    uploadFile("Fail rename project");
                }





                //check url detail project


                try
                {
                    driver.Url = "http://localhost:8080/projects";
                    Delay(2);

                    var cardItems = driver.FindElements(By.CssSelector("p[class='card-text small small']"));
                    cardItems[cardItems.Count - 1].Click();
                    Delay(2);

                    if (driver.Url.ToString() == "http://localhost:8080/projects")
                    {
                        uploadFile("{ Click card project không đến trang yêu cầu }");
                    }
                    else
                    {
                        uploadFile("{ Show project detail success }");
                    }
                }
                catch
                {
                    uploadFile("Fail click card project");
                }


                //check info project
                try
                {
                    driver.Url = "http://localhost:8080/projects";
                    Delay(2);


                    var cardItems = driver.FindElements(By.CssSelector("p[class='card-text small small']"));
                    cardItems[cardItems.Count - 1].Click();
                    Delay(2);

                    if (driver.Url.ToString() == "http://localhost:8080/projects")
                    {
                        uploadFile("{ Click card project không đến trang yêu cầu }");
                    }
                    else
                    {
                        uploadFile("{ Show project detail success }");
                    }


                    string url3 = driver.Url.ToString();

                    driver.FindElement(By.CssSelector("i[class='fas fa-info-circle fa-3x red red']")).Click();
                    Delay(2);
                    string url4 = driver.Url.ToString();

                    if (url3 == url4)
                    {
                        uploadFile("{ Click view info project failed }");
                    }
                    else
                    {
                        uploadFile("{ View info project success }");
                    }
                }
                catch
                {
                    uploadFile("{ Fail click info project }");
                }


                //check update info project

                try
                {
                    driver.Url = "http://localhost:8080/projects";
                    Delay(2);

                    var cardItems = driver.FindElements(By.CssSelector("p[class='card-text small small']"));
                    cardItems[cardItems.Count - 1].Click();
                    Delay(2);


                    string url1 = driver.Url.ToString();
                    driver.FindElement(By.CssSelector("i[class='fas fa-info-circle fa-3x red red']")).Click();
                    Delay(2);

                    string nameRd1 = RandomString(5);
                    driver.FindElement(By.CssSelector("input[placeholder='Name project']")).SendKeys(nameRd1);
                    Delay(2);
                    driver.FindElement(By.CssSelector("textarea[placeholder='Descritions']")).SendKeys(RandomString(5));
                    Delay(2);
                    driver.FindElement(By.CssSelector("input[type='date']")).SendKeys("22021990");
                    Delay(2);

                    driver.FindElement(By.CssSelector("button[class='btn btn-success btn-rounded ripple-surface']")).Click();
                    Delay(2);
                    string url2 = driver.Url.ToString();
                    driver.Url = url1;
                    Delay(5);
                    if (driver.FindElement(By.TagName("body")).Text.IndexOf(nameRd1) == -1)
                    {
                        uploadFile("{ Update info project error }");
                    }
                    else
                    {
                        uploadFile("{ Update info project sucess }");
                    }
                }
                catch (Exception ex)
                {
                    uploadFile("{ fail check update profile }");
                }

                //check button archive or delete

                try
                {
                    driver.Url = "http://localhost:8080/projects";
                    Delay(2);

                    var items = driver.FindElements(By.TagName("a"));
                    items[items.Count - 1].Click();
                    Delay(2);

                    driver.FindElement(By.CssSelector("i[class='fas fa-trash']")).Click();
                    Delay(2);

                    if (driver.Url.ToString() == "http://localhost:8080/projects")
                    {
                        uploadFile("Click button archive or delete không đến trang yêu cầu");
                    }

                    driver.FindElement(By.CssSelector("button[class='btn btn-outline-danger btn-rounded ripple-surface']")).Click();
                    Delay(2);

                    if (driver.Url.ToString() != "http://localhost:8080/projects")
                    {
                        uploadFile("{ Fail button archive or delete }");
                    }else
                    {
                        uploadFile("{ Archive or delete success }");
                    }
                }
                catch
                {
                    uploadFile("{ Fail button archive or delete }");
                }
            }
            finally
            {

                driver.Close();
                driver.Quit();
            }


        }

        public static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZzxcvbnmasdfghjklqwertyuiop123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        void TestLogin()
        {
            int numberCount = 1;
            string[] successMessage1 = { "Test Login :" };
            File.AppendAllLines("data//out.txt", successMessage1);
            foreach (var user in users)
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                ChromeDriver driver = new ChromeDriver(chromeOptions);
                driver.Url = "http://localhost:8080/login";
                driver.Navigate();
                Delay(4);
                driver.Manage().Window.Maximize();

                driver.FindElement(By.CssSelector("input:first-child")).SendKeys(user.Email);
                Delay(3);

                driver.FindElement(By.CssSelector("input[type='password']")).SendKeys(user.Password);
                Delay(3);


                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                Delay(4);
                try
                {
                    // MessageBox.Show(driver.FindElement(By.ClassName("c-toast--error")).Text);

                    if(user.Message == null)
                    {
                        if(driver.Url.ToString() != "http://localhost:8080/projects")
                        {
                            string url = driver.Url.ToString();
                            string[] errUrl = { url + "( Not excuted)" };
                            File.AppendAllLines("data//out.txt", errUrl);
                        }else
                        {
                            if(numberCount == 1)
                            {
                                string[] successMessage = { "{ Trường hợp " + numberCount + " thành công}" };
                                File.AppendAllLines("data//out.txt", successMessage);
                            }
                        }
                    }
                    else
                    {
                        if (driver.FindElement(By.ClassName("c-toast--error")).Text != user.Message)
                        {
                            string url = driver.Url.ToString();
                            string[] errUrl = { url + "{" + user.Message +"}" };
                            File.AppendAllLines("data//out.txt", errUrl);
                        }else
                        {
                            string[] successMessage2 = { "{ Trường hợp " + numberCount + " thành công}" };
                            File.AppendAllLines("data//out.txt", successMessage2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error");
                }
                finally
                {
                    driver.Close();
                    driver.Quit();
                }
                numberCount++;
            }
        }
        void TestTask()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            ChromeDriver driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            //
            void uploadFile(string message)
            {
                string[] errUrl8 = {message };
                File.AppendAllLines("data//out.txt", errUrl8);
            }
            uploadFile("Test Task:");
            try
            {
                driver.Url = "http://localhost:8080/login";
                driver.Navigate();
                Delay(2);

                driver.FindElement(By.CssSelector("input:first-child")).SendKeys("dangtung789.td@gmail.com");
                Delay(2);

                driver.FindElement(By.CssSelector("input[type='password']")).SendKeys("123456");
                Delay(2);

                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                Delay(7);


                //Test create task 

                try
                {
                    var items = driver.FindElements(By.ClassName("card"));
                    items[items.Count - 1].Click();
                    Delay(2);
                }
                catch(Exception e)
                {
                    uploadFile("Project Failed");
                    driver.Close();
                    driver.Quit();
                }

                driver.FindElement(By.CssSelector("div[class='col-sm mb-1']")).Click();
                Delay(2);
             



                //click new task

                driver.FindElement(By.CssSelector("button[class='btn btn-success btn-lg btn-rounded ripple-surface']")).Click();
                Delay(2);




                if (string.IsNullOrEmpty(driver.FindElement(By.CssSelector("button[class='btn btn-success btn-rounded ripple-surface']")).Text.ToString()))
                {
                    uploadFile("{ Button create new task error }");
                }
                else
                {
                    var inputNameTask = driver.FindElements(By.CssSelector("input[class='form-control pd-3']"));
                    var nameTask = RandomString(7);
                    inputNameTask[0].SendKeys(nameTask);
                    Delay(2);

                    var inputDescriptionTask = driver.FindElements(By.CssSelector("textarea[class='form-control pd-3']"));
                    var descriptionTask = RandomString(10);
                    inputDescriptionTask[0].SendKeys(descriptionTask);
                    Delay(2);

                    driver.FindElement(By.CssSelector("button[class='btn btn-success btn-rounded ripple-surface']")).Click();
                    Delay(2);

                    string contentBody = driver.FindElement(By.TagName("body")).Text;
                    if (contentBody.IndexOf(nameTask) != -1)
                    {
                        uploadFile("{ Create new task success }");
                    }
                    else
                    {
                        uploadFile("{ Error create new task }");
                    }

                    //    //check edit task

                    try
                    {

                        driver.FindElement(By.CssSelector("div[class='col-md-11']")).Click();
                        Delay(2);
                        string nameTaskRd = RandomString(5);
                        driver.FindElement(By.CssSelector("input[class='form-control active pd-3']")).SendKeys(nameTaskRd);
                        Delay(2);
                        driver.FindElement(By.CssSelector("textarea[class='form-control active pd-3']")).SendKeys(RandomString(6));
                        Delay(2);
                        var btn = driver.FindElements(By.CssSelector("button[class='btn btn-success btn-rounded ripple-surface']"));
                        btn[btn.Count - 1].Click();
                        Delay(4);

                        if (driver.FindElement(By.TagName("body")).Text.ToString().IndexOf(nameTaskRd) == -1)
                        {
                            uploadFile("{ Edit task error }");
                        }
                        else
                        {
                            uploadFile("{ Edit task success }");
                        }

                   

                    }
                    catch (Exception ex)
                    {
                        uploadFile("{ Edit task error }");
                    }

                    //check save task
                    try
                    {
                        var cardHeader = driver.FindElements(By.ClassName("card-header"));
                        cardHeader[0].Click();
                        Delay(2);

                        string urlCheck = driver.Url.ToString();
                        string contentDelete = driver.FindElement(By.CssSelector("div[class='col-md-11']")).Text;

                        driver.FindElement(By.CssSelector("div[class='col-md-11']")).Click();

                        Delay(2);
                        driver.FindElement(By.CssSelector("button[class='btn btn-danger btn-rounded ripple-surface']")).Click();
                        Delay(2);

                        if (driver.FindElement(By.Id("app")).Text.ToString().IndexOf(contentDelete) != -1)
                        {
                            uploadFile("{ Save task error }");
                        }
                        else
                        {
                            uploadFile("{ Save task success }");
                        }
                    }
                    catch (Exception e)
                    {
                        uploadFile("{ Save task failed }");
                    }

                }

            }
            catch (Exception ex)
            {
                uploadFile("Error task");
                driver.Close();
                driver.Quit();
            }
            finally
            {
                driver.Close();
                driver.Quit();
            }







        }
        void Delay(int delay)
        {
            while (delay > 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                delay--;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TestProject();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TestTask();
        }
    }
}
