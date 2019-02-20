﻿using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using GraphQL.Business.Models;
using GraphQL.Business.Models.Parameters;

namespace GraphQL.Business.Commands.Messages
{
    public class MessageCreated : Command<MessageCreatedParameter, Message>, IMessageCreated
    {
        private readonly ISubject<Message> _subject;

        public MessageCreated()
        {
            _subject = new ReplaySubject<Message>(1);
        }

        public override IObservable<Message> Execute(MessageCreatedParameter input)
        {
            return _subject.Where(message => FilterByAuthor(message, input.Author));
        }

        public IObservable<Message> Execute(Message input)
        {
            return Observable.Return(input)
                .Do(message => _subject.OnNext(input));
        }

        private static bool FilterByAuthor(Message message, string author)
        {
            if (!string.IsNullOrWhiteSpace(author))
            {
                return message.Author == author;
            }

            return true;
        }
    }
}