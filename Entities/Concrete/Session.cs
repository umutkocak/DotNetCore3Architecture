using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.Concrete
{
    public class Session :IEntity
    {
        /// <summary>
        /// JSON Web Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh Token. Oturum sonlanm��sa, RefreshToken ile yeni bir AccessToken elde edilir.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Oturumun sahip oldu�u User ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Oturumun ba�lat�ld��� IP Adresi
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Kullan�c� Browser Bilgileri
        /// </summary>
        public string UserAgent { get; set; }
  
      
        /// <summary>
        /// Oturumun ne zaman sonlanaca�� bilgisi
        /// </summary>
        public DateTime SessionExpireDate { get; set; }
        public DateTime CreatedDate { get; set; } 
        /// <summary>
        /// Oturumun, aktif olup/olmad��� kontorl�
        /// </summary>
        public bool IsActive { get; set; }
     
   

        public void FillClientDetails(HttpRequest httpRequest)
        {
            this.IpAddress = httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();
            this.UserAgent = httpRequest.Headers["User-Agent"].ToString();
        }
    }
}

