﻿using Microsoft.Extensions.Logging;

namespace DandE.DocumentHandler
{
    public abstract class Card
    {
        protected static ILogger Logger { get; set; }

        public Card(byte[] bytes, ILogger logger)
        {
            Logger = logger;
            Populate(bytes);
        }

        private void Populate(byte[] bytes)
        {
            this.Bytes = bytes;

            var result = this.Parser.ParseDocument(bytes);

            this.Text = result.Text;

            this.Title = result.Title;
        }

        public Card(string fileName, ILogger logger)
        {
            Logger = logger;
            Populate(File.ReadAllBytes(fileName));
        }


        public abstract DocumentParser Parser { get; }

        public string Text { get; set; }

        public byte[] Bytes { get; set; }

        public string Title { get; set; }

        public static bool DocumentIsPermittedForCurrentUser(object currentUser, string fileName, ILogger logger)
        {
            try
            {
                File.ReadAllBytes(fileName);

             
            }
            catch (ForbiddenDocumentException)
            {
                return false;
            }
            catch (FileNotFoundException fileNotFound)
            {
                logger.LogInformation($"Could not find file '{fileName}'");
                logger.LogError(fileNotFound.ToString());

                throw;
            }

            return true;
        }
    }
}