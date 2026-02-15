<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.IO;
using System.Web;


    //public class FileUploadHandler : IHttpHandler
    //{

    //    public void ProcessRequest(HttpContext context)
    //    {
    //        string newFileName=String.Empty;
    //        if (context.Request.Files.Count > 0)
    //        {
    //            HttpFileCollection files = context.Request.Files;
    //            for (int i = 0; i < files.Count; i++)
    //            {
    //                HttpPostedFile file = files[i];
    //                var guidFileName = new Guid();
    //                var fileext = Path.GetExtension(file.FileName);
    //                newFileName = guidFileName + "." + fileext;
    //                //string fname = context.Server.MapPath("~/UploadImg/" + file.FileName);
    //                string fname = context.Server.MapPath("~/UploadImg/" + newFileName);
    //                file.SaveAs(fname);
    //            }
    //        }
    //        context.Response.ContentType = "text/plain";
    //        context.Response.Write(newFileName);
    //    }

    //    public bool IsReusable
    //    {
    //        get
    //        {
    //            return false;
    //        }
    //    }
    //}

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

public class FileUploadHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        // Check if the request contains a file
        if (context.Request.Files.Count > 0)
        {
            // Fetch the uploaded file
            HttpPostedFile postedFile = context.Request.Files[0];

            // Get the original file name
            string originalFileName = Path.GetFileName(postedFile.FileName);

            // Define the directory path where the file will be saved
            string dDrivePath = @"D:\UBL_DepositSlip_Image\";

            // Ensure the directory exists; if not, create it
            if (!Directory.Exists(dDrivePath))
            {
                Directory.CreateDirectory(dDrivePath);
            }

            // Generate a new unique file name with the same extension
            string uniqueFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(postedFile.FileName);

            // Combine the directory path and unique file name
            string fullFilePath = Path.Combine(dDrivePath, uniqueFileName);

            try
            {
                // Save the file to the specified folder
                postedFile.SaveAs(fullFilePath);

                // Prepare the JSON response
                string jsonResponse = new JavaScriptSerializer().Serialize(new
                {
                    name = originalFileName,
                    dbfilename = uniqueFileName
                });

                // Set the HTTP response
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonResponse);
            }
            catch (Exception ex)
            {
                // Handle errors during file upload
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                context.Response.Write(new JavaScriptSerializer().Serialize(new
                {
                    error = "An error occurred while uploading the file.",
                    message = ex.Message
                }));
            }
            finally
            {
                // End the response
                context.Response.End();
            }
        }
        else
        {
            // Handle case when no file is uploaded
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            context.Response.Write(new JavaScriptSerializer().Serialize(new
            {
                error = "No file uploaded."
            }));
            context.Response.End();
        }
    }

    
    
    //public void ProcessRequest(HttpContext context)
    //{
    //    //Check if Request is to Upload the File.
    //    if (context.Request.Files.Count > 0)
    //    {
    //        //Fetch the Uploaded File.
    //        HttpPostedFile postedFile = context.Request.Files[0];

    //        //Set the Folder Path.
    //        string fileName = Path.GetFileName(postedFile.FileName);
    //        //   string folderPath = @"D:\UploadMeetingDocument\"; 

    //        string dDrivePath = @"D:\UBLImage\";
    //        string filePath = Path.Combine(dDrivePath);

    //        //  string folderPath = context.Server.MapPath("~/UpLoadFile/");

    //        string folderPath = filePath;
          

    //        var guidFileName = Guid.NewGuid().ToString("N");
    //        var fileext = Path.GetExtension(postedFile.FileName);
    //        string newFileName = guidFileName + fileext;

    //        //Save the File in Folder.
    //        //postedFile.SaveAs(folderPath + fileName);
    //        postedFile.SaveAs(folderPath + newFileName);

    //        //Send File details in a JSON Response.
    //        string json = new JavaScriptSerializer().Serialize(
    //            new
    //            {
    //                name = fileName,
    //                dbfilename = newFileName
    //            });
    //        context.Response.StatusCode = (int)HttpStatusCode.OK;
    //        context.Response.ContentType = "text/json";
    //        context.Response.Write(json);
    //        context.Response.End();
    //    }
    //}

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}