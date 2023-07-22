.PHONY: run test clean

G++ = g++

CC = $(G++)
CFLAGS = -Werror -Wall -Wextra -Wpedantic
SFML_FLAGS = -lsfml-graphics -lsfml-window -lsfml-system -lsfml-audio

TARGET = battleships
TEST_TARGET = battleships_test
LIB_TARGET = sfml_wrapper

OBJ_PATH = obj/
SRC_PATH = src/
TEST_PATH = test/
BUILD_PATH = build/
SOURCES_PATH = $(SRC_PATH)$(TARGET)/sources/
HEADERS_PATH = $(SRC_PATH)$(TARGET)/headers/
LIB_SOURCES_PATH = $(SRC_PATH)$(TARGET)/sources/
LIB_HEADRS_PATH = $(SRC_PATJ)$(LIB_TARGET)/headers/

SOURCES = $(wildcard $(SOURCES_PATH)*.cpp)
LIB_SOURCES = $(wildcard $(LIB_SOURCES_PATH)*.cpp)

OBJ = $(patsubst $(SOURCES_PATH)%.cpp, $(OBJ_PATH)$(TARGET)%.o, $(SOURCES))
LIB_OBJ = $(patsubst $(LIB_SOURCES_PATH)%.cpp, $(OBJ_PATH)$(LIB_TARGET)%.o, $(LIB_SOURCES))

EXECUTABLE=$(BUILD_PATH)$(TARGET).out
LIBRARY=$(BUILD_PATH)$(LIB_TARGET).a
EXECUTABLE_TEST=$(BUILD_PATH)$(TEST_TARGET).out

debug: CFLAGS = -g $(CFLAGS) 

all: $(EXECUTABLE) $(LIBRARY) $(EXECUTABLE_TEST)

run: $(EXECUTABLE) $(LIBRARY)
	./$<

debug: $(EXECUTABLE) $(LIBRARY)
	./$<

test: $(EXECUTABLE_TEST) $(LIBRARY)
	./$<

clean :
	rm -f $(OBJ) $(LIB_OBJ) $(EXECUTABLE) $(LIBRARY) $(EXECUTABLE_TEST)

$(EXECUTABLE) : $(OBJ)
	$(CC) $(CFLAGS) $< -o $@ $(SFML_FLAGS)

$(OBJ)%.o : $(SOURCES)%.cpp
	$(CC) $(CFLAGS) -I $(HEADERS_PATH) -c $< -o $@

$(LIBRARY) : $(LIB_OBJ)
	ar rcs $@ $^

$(LIB_OBJ)%.o : $(LIB_SOURCES)%.cpp
	$(CC) $(CFLAGS) -I $(LIB_HEADRS_PATH) -c $< -o $@ $(SFML_FLAGS)