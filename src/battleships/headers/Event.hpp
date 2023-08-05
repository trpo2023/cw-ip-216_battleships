#pragma once

#include <functional>
#include <list>

namespace bs
{
    template <typename T>
    class Event
    {
    private:
        std::list<std::function<void(T)>> listeners;

    public:
        void addListener(std::function<void(T)> listener)
        {
            listeners.push_back(listener);
        }
        void removeListener(std::function<void(T)> listener)
        {
            listeners.erase(std::remove(listeners.begin(), listeners.end(), &listener));
        }
        void invoke(T data)
        {
            for (auto &item : listeners)
            {
                item(data);
            }
        }
    };

}
