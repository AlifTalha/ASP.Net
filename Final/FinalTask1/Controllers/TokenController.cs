using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FinalTaskLab1.Models;

namespace FinalTaskLab1.Controllers
{
    public class TokenController : Controller
    {
        private static List<Token> _tokens = new List<Token>();

        public ActionResult List()
        {
            var tokens = _tokens.OrderByDescending(t => t.CreatedAt).ToList();
            if (!tokens.Any())
            {
                TempData["Info"] = "No tokens available.";
            }
            return View(tokens);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string counterType)
        {
            if (string.IsNullOrWhiteSpace(counterType))
            {
                TempData["Error"] = "Counter type is required.";
                return RedirectToAction("Create");
            }

            var validCounters = new[] { "medical", "tourist", "business", "go" };
            if (!validCounters.Contains(counterType.ToLower()))
            {
                TempData["Error"] = "Invalid counter type.";
                return RedirectToAction("Create");
            }

            int maxTokensPerCounter = 25;
            int issuedTokens = _tokens.Count(t => t.CounterType.Equals(counterType, StringComparison.OrdinalIgnoreCase)
                                                   && t.CreatedAt.Date == DateTime.Today);
            if (issuedTokens >= maxTokensPerCounter)
            {
                TempData["Error"] = $"{counterType.ToUpper()} counter has reached its max token limit ({maxTokensPerCounter}).";
                return RedirectToAction("Create");
            }

            string tokenPrefix = counterType.Substring(0, 2).ToUpper();
            int globalTokenNumber = _tokens.Count + 1;
            string newToken = $"{tokenPrefix}-{globalTokenNumber}";

            var token = new Token
            {
                Id = globalTokenNumber,
                TokenNumber = newToken,
                CounterType = counterType,
                CreatedAt = DateTime.Now
            };

            _tokens.Add(token);

            TempData["Success"] = $"Token generated successfully: {newToken}";
            return RedirectToAction("List");
        }
    }
}
