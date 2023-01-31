using MOON.Entities.Dashboard;
using MOON.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MOON.Web
{
    public partial class _Default : Page
    {
        private int pageSize = 6;
        private int pageIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindArticle();
            }
        }

        /// <summary>
        /// To show all article cards in the article-lists. 
        /// </summary>

        private void BindArticle()
        {
            ArticleService articleService = new ArticleService();
            if (Request.QueryString["category"] != null)
            {

                DataTable dt = articleService.ArticleFilterByCategory(Request.QueryString["category"].ToString());
                BindRepeater(dt);
            }
            else if (Request.QueryString["keyword"] != null)
            {
                DataTable dt = articleService.ArticleFilterBySearch(Request.QueryString["keyword"].ToString());
                BindRepeater(dt);

            }
            else if (Request.QueryString["keyword"] != null && Request.QueryString["category"] != null)
            {
                DataTable dt = articleService.ArticleFilterByBoth(Request.QueryString["keyword"].ToString(), Request.QueryString["category"].ToString());
                BindRepeater(dt);
            }
            else
            {
                DataTable dt = articleService.ShowCard();
                BindRepeater(dt);
            }
        }

        private void BindRepeater(DataTable dt)
        {
            int recordCount = dt.Rows.Count;
            int pageCount = recordCount / pageSize;
            if (recordCount % pageSize > 0)
            {
                pageCount += 1;
            }

            DataTable dtPage = dt.Clone();
            int startIndex = pageIndex * pageSize;
            int endIndex = startIndex + pageSize;
            if (endIndex > recordCount)
            {
                endIndex = recordCount;
            }
            for (int i = startIndex; i < endIndex; i++)
            {
                dtPage.ImportRow(dt.Rows[i]);
            }

            if(!(recordCount >= pageSize))
            {
                btnNext.Visible = false;
                btnPrev.Visible = false;
            }

            reptrArticles.DataSource = dtPage;
            reptrArticles.DataBind();
        }

        protected void btnPrevClick(object sender, EventArgs e)
        {
            pageIndex--;
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }
            BindArticle();
        }

        protected void btnNextClick(object sender, EventArgs e)
        {
            ArticleService articleService = new ArticleService();
            DataTable dt = articleService.ShowCard();
            int recordCount = dt.Rows.Count;
            int pageCount = recordCount / pageSize;
            if (recordCount % pageSize > 0)
            {
                pageCount += 1;
            }

            pageIndex++;
            if (pageIndex >= pageCount)
            {
                pageIndex = pageCount - 1;
            }
            BindArticle();
        }


        /// <summary>
        /// To filter as category items and display at index page.
        /// </summary>
        protected void ReptrDataItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Slug")
            {
                if (Request.QueryString["keyword"] != null)
                {
                    Response.Redirect("~/Default?keyword=" + Request.QueryString["keyword"].ToString() + "&category=" + e.CommandArgument);
                }
                else
                {
                    Response.Redirect("~/Default?category=" + e.CommandArgument);
                }
            }
        }

        /// <summary>
        /// To view specific article card detail.
        /// </summary>
        protected void ReptrCardDataItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect("~/Views/Frontend/Detail.aspx?slug=" + e.CommandArgument);
        }


        /// <summary>
        /// If there is no article in the database list, it will shows no record in the article blk. 
        /// </summary>
        protected void ReptrCardItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (reptrArticles.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlGenericControl noRecordsDiv = (e.Item.FindControl("NoRecords") as HtmlGenericControl);
                    if (noRecordsDiv != null)
                    {
                        noRecordsDiv.Visible = true;
                    }
                }
            }
        }

        public List<CategoryEntity> GetCategories()
        {
            CategoryService categoryService = new CategoryService();
            List<CategoryEntity> cc = categoryService.GAC();
            return cc;
        }
    }
}