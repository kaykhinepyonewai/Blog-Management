using MOON.Entities.Comment;
using MOON.Entities.Dashboard;
using MOON.Entities.Like;
using MOON.Services.Comment;
using MOON.Services.Dashboard;
using MOON.Services.Like;
using MOON.Services.Report;
using MOON.Services.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Frontend
{
    public partial class Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["slug"] != null)
                {
                    hdSlug.Value = Request.QueryString["slug"].ToString();
                    BindReportList();
                    BindCommentData();
                    BindReportLists();
                    LoadedInfo();
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
                ViewersLoader();
            }
        }

        private void ViewersLoader()
        {
            string slug =  Request.QueryString["slug"].ToString();
            ArticleService articleService = new ArticleService();
            LikeService likeService = new LikeService();
            DataTable id = articleService.GetSpecific("Slug", slug);
            DataTable dt = likeService.ReactionViewers(Convert.ToInt32(id.Rows[0]["ArticleId"].ToString()));
            reptrReactionViewers.DataSource = dt;
            reptrReactionViewers.DataBind();
        }

        /// <summary>
        /// To display current user's profile and username in comment box.
        /// </summary>
        private void LoadedInfo()
        {
            if (Session.Count != 0)
            {
                string[] user = (string[])Session["Users"];
                UserService userService = new UserService();
                DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
                imgProfile.ImageUrl = dt.Rows[0]["Profile"].ToString();
                username.InnerText = dt.Rows[0]["Username"].ToString();
            }
        }

        /// <summary>
        /// To display all reports as list
        /// </summary>
        private void BindReportList()
        {
            ReportService reportService = new ReportService();
            DataTable dt = reportService.GetAll();
            reptrReportLists.DataSource = dt;
            reptrReportLists.DataBind();
        }

        private void BindCommentData()
        {
            ArticleService articleService = new ArticleService();
            CommentService commentService = new CommentService();
            DataTable dt = articleService.GetSpecific("Slug", Request.QueryString["slug"].ToString());
            int articleid = Convert.ToInt32(dt.Rows[0]["ArticleId"].ToString());
            DataTable commentdt = commentService.GetSpecific("ArticleId", articleid);
            msgCount.InnerText = commentdt.Rows.Count.ToString();
            msgCounts.InnerText = commentdt.Rows.Count.ToString();
            reptrCommentLists.DataSource = commentdt;
            reptrCommentLists.DataBind();
        }

        /// <summary>
        ///To display all related articles as a list for a specific article.
        /// </summary>
        private void BindReportLists()
        {
            ArticleService articleService = new ArticleService();
            DataTable dt = articleService.GetSpecific("Slug", Request.QueryString["slug"].ToString());
            int categoryid = Convert.ToInt32(dt.Rows[0]["CategoryId"].ToString());
            DataTable relateddt = articleService.GetRelated(categoryid, Request.QueryString["slug"].ToString());
            reptrRelatedLists.DataSource = relateddt;
            reptrRelatedLists.DataBind();
        }

        /// <summary>
        /// allow users to send a comment on each aticle except guest.
        /// </summary>
        protected void BtnCommentSendClick(object sender, EventArgs e)
        {
            if (Session["Users"] != null)
            {
                if (txtComment.ToString().Length >= 2)
                {
                    ArticleService articleService = new ArticleService();
                    CommentService commentService = new CommentService();
                    DataTable dt = articleService.GetSpecific("Slug", Request.QueryString["slug"].ToString());
                    int articleid = Convert.ToInt32(dt.Rows[0]["ArticleId"].ToString());
                    CommentEntity commentEntity = CreateComment(articleid);
                    bool success = commentService.Insert(commentEntity);
                    if (success)
                    {
                        txtComment.Text = string.Empty;
                        BindCommentData();
                    }
                }
                else
                {
                    lblErrorMsg.Text = "Comment min length should be 2.";
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        /// <summary>
        /// To filter all articles by category and display them on the index page.
        /// </summary>
        protected void ReptrDataItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Slug")
            {
                Response.Redirect("~/Default?category=" + e.CommandArgument);
            }
        }

        /// <summary>
        /// allow users to report all articles except guest.
        /// </summary>
        protected void ReptrReportDataItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (Session["Users"] != null)
            {
                if (e.CommandName == "ReportId")
                {
                    ArticleService articleService = new ArticleService();
                    DataTable dt = articleService.GetSpecific("Slug", Request.QueryString["slug"].ToString());
                    int articleid = Convert.ToInt32(dt.Rows[0]["ArticleId"].ToString());
                    ArticleEntity articleEntity = UpdateData(Convert.ToInt32(e.CommandArgument), articleid);
                    bool success = articleService.UpdateArticleReport(articleEntity);
                    if(success)
                    {
                        reportAlert.Visible = true;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void ReptrRelatedItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RelatedSlug")
            {
                Response.Redirect("~/Views/Frontend/Detail.aspx?slug=" + e.CommandArgument);
            }
        }

        /// <summary>
        /// To update report in article database. 
        /// </summary>
        private ArticleEntity UpdateData(int id, int articleid)
        {
            ArticleEntity articleEntity = new ArticleEntity();
            articleEntity.ReportId = id;
            articleEntity.ReportStatus = 1;
            articleEntity.ReportAt = DateTime.Now;
            articleEntity.ArticleId = articleid;
            return articleEntity;
        }

        /// <summary>
        /// To store in comment database. 
        /// </summary>
        private CommentEntity CreateComment(int articleId)
        {
            CommentEntity commentEntity = new CommentEntity();
            commentEntity.CommentId = Convert.ToInt32(hdnCommentId.Value);
            commentEntity.UserId = Convert.ToInt32(Session["UserId"].ToString());
            commentEntity.ArticleId = Convert.ToInt32(articleId);
            commentEntity.Message = txtComment.Text.ToString();
            commentEntity.CreatedAt = DateTime.Now;
            commentEntity.UpdatedAt = DateTime.Now;
            return commentEntity;
        }


        /// <summary>
        /// If there is no specific article's comment in the database list, it will shows no record in the comment side. 
        /// </summary>
        protected void ReptrCommentItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (reptrCommentLists.Items.Count < 1)
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

        /// <summary>
        /// If there is no specific related article in the database list, it will shows no record in the related list blk. 
        /// </summary>
        protected void ReptrRelatedItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (reptrRelatedLists.Items.Count < 1)
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

        public List<PhotoEntity> GTImage()
        {
            PhotoService photoService = new PhotoService();
            List<PhotoEntity> cc = photoService.GetImageBySlug((hdSlug.Value));
            return cc;
        }

        public List<ArticleEntity> GATDetailBySlug()
        {
            
            ArticleService articleService = new ArticleService();
            List<ArticleEntity> cc = articleService.GATDetailBySlug((hdSlug.Value));

            return cc;
        }

        public List<CategoryEntity> GetCategories()
        {
            CategoryService categoryService = new CategoryService();
            List<CategoryEntity> cc = categoryService.GAC();
            return cc;
        }


        protected void btnlnkDeleteClick(object sender, EventArgs e)
        {
            string slug = Request.QueryString["slug"].ToString();
            ArticleService articleService = new ArticleService();
            PhotoService photoService = new PhotoService();
            DataTable id = articleService.GetSpecific("Slug", slug);
            bool success = articleService.Delete(Convert.ToInt32(id.Rows[0]["ArticleId"].ToString()));
            bool success1 = photoService.DeleteAritcle(Convert.ToInt32(id.Rows[0]["ArticleId"].ToString()));

            if (success)
            {
                Response.Redirect("~/Default.aspx");
            }
        }


            public int GATPermit()
            {
            int userid = Convert.ToInt32(Session["UserId"]);
            int countPermit = 0;
            ArticleService articleService = new ArticleService();
            countPermit = articleService.CountPermit((hdSlug.Value), userid);
            return countPermit;
        }


        protected void UnLikeClick(object sender, EventArgs e)
        {
            bool success = false;
            string val = txtLikedHidden.Value;
            string val1 = txtLikedHidden1.Value;
            LikeEntity likeEntity = new LikeEntity();
            int userid = Convert.ToInt32(Session["UserId"]);
            if(userid == 0)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                likeEntity.UserId = Convert.ToInt32(userid);
                likeEntity.ArticleId = Convert.ToInt32(val);
                LikeService likeService = new LikeService();
                success = likeService.Delete(likeEntity);
                Response.Write("<script>history.go(-1)</script>");
            }
   
        }

        public int GetRoleId()
        {
            int userid = Convert.ToInt32(Session["UserId"]);
            int roleId;
            UserService userService = new UserService();
           DataTable roleIdt = userService.GetRoleId(userid);
            roleId = Convert.ToInt32(roleIdt.Rows[0]["RoleId"].ToString());
            return roleId;
        }

        protected void LikeClick(object sender, EventArgs e)
        {
            bool success = false;
            string val = txtLikedHidden.Value;
            string val1 = txtLikedHidden1.Value;
            LikeEntity likeEntity = new LikeEntity();
            
            int userid = Convert.ToInt32(Session["UserId"]);

            if (userid == 0)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                likeEntity.UserId = Convert.ToInt32(userid);
                likeEntity.ArticleId = Convert.ToInt32(val);
                LikeService likeService = new LikeService();
                success = likeService.Insert(likeEntity);
                Response.Write("<script>history.go(-1)</script>");
            }
         }

        public int CountButton(int id)
        {
            int userid = Convert.ToInt32(Session["UserId"]);
            int countLike = 0;
            LikeService likeService = new LikeService();
            countLike = likeService.CountButton(id, userid);
            return countLike;
        }

        public int Count(int id)
        {
            int countLike = 0;
            LikeService likeService = new LikeService();
            countLike = likeService.CountLike(id);
            return countLike;
        }


        public int CountLike()
        {
            string slug = Request.QueryString["slug"].ToString();
            int countLike = 0;
            ArticleService articleService = new ArticleService();
            LikeService likeService = new LikeService();
            DataTable id = articleService.GetSpecific("Slug", slug);
            countLike = likeService.CountLike(Convert.ToInt32(id.Rows[0]["ArticleId"].ToString()));
            return countLike;
        }

        public string GetTitle()
        {
            string slug = Request.QueryString["slug"].ToString();
            string title;
            ArticleService articleService = new ArticleService();
            LikeService likeService = new LikeService();
            DataTable experctdt = articleService.GetSpecific("Slug", slug);
            title = experctdt.Rows[0]["Title"].ToString();
            return title;
        }

    }
}