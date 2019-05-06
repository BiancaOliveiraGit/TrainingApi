using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TrainingApi.ErrorMiddleware;

namespace TrainingApi.Data
{
    public partial class Repository 
    {
        public VideoLibrary GetVideoById(int id, ILogger<VideoLibrary> logger)
        {
            var item = new VideoLibrary();
            try
            {
                item = _appDbContext.VideoLibraries.Where(w => w.VideoLibraryId == id)
                                        .Select(s => s).FirstOrDefault();
                return item;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetVideoById: {id}");
            }
            return item;
        }

        public IEnumerable<VideoLibrary> GetVideoLibraries(ILogger<VideoLibrary> logger)
        {
            var list = new List<VideoLibrary>();
            try
            {
                list = _appDbContext.VideoLibraries.Select(s => s).ToList();
                return list;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in GetVideoLibraries");
            }
            return list;
        }

        public VideoLibrary PostNewVideo(VideoLibrary newVideo, ILogger<VideoLibrary> logger)
        {
            try
            {
                //check that video doesn't exist
                var exists = _appDbContext.VideoLibraries.Where(w => w.VideoUrl == newVideo.VideoUrl && w.DoNotUse == false)
                                                  .Select(s => s).FirstOrDefault();
                if (exists != null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Video link already exists");

                var item = _appDbContext.Add(newVideo);
                item.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                var isOk = _appDbContext.SaveChanges();

                return item.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in PostNewVideo: {newVideo.VideoUrl}");
                throw e;
            }
        }

        public VideoLibrary UpdateVideo(int id, VideoLibrary updateVideo, ILogger<VideoLibrary> logger)
        {
            try
            {
                //check that video  exists
                var existingVid = _appDbContext.VideoLibraries.Where(w => w.VideoLibraryId == updateVideo.VideoLibraryId)
                                                  .Select(s => s).FirstOrDefault();
                if (existingVid != null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, string.Format("VideoLibraryID {0},- {1} Doesn't Exist in system", updateVideo.VideoLibraryId, updateVideo.AltTag));

                //update video
                existingVid.AltTag = updateVideo.AltTag;
                existingVid.ModifiedDate = DateTime.Now;
                existingVid.VideoUrl = updateVideo.VideoUrl;
                existingVid.DoNotUse = updateVideo.DoNotUse;

                var isOk = _appDbContext.SaveChanges();

                return existingVid;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in UpdateVideo: {updateVideo.VideoLibraryId} - {updateVideo.VideoUrl}");
            }
            return updateVideo;
        }
    }
}
