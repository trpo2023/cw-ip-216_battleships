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

LIB_SOURCES_PATH = $(SRC_PATH)$(LIB_TARGET)/sources/
LIB_HEADRS_PATH = $(SRC_PATH)$(LIB_TARGET)/headers/

ALL_HEADERS_INCLUDE = -I $(LIB_HEADRS_PATH) -I $(HEADERS_PATH)


SOURCES = $(wildcard $(SOURCES_PATH)*.cpp)
LIB_SOURCES = $(wildcard $(LIB_SOURCES_PATH)*.cpp)

OBJ = $(patsubst $(SOURCES_PATH)%.cpp, $(OBJ_PATH)$(TARGET)/%.o, $(SOURCES))
LIB_OBJ = $(patsubst $(LIB_SOURCES_PATH)%.cpp, $(OBJ_PATH)$(LIB_TARGET)/%.o, $(LIB_SOURCES))

EXECUTABLE=$(BUILD_PATH)$(TARGET).out
LIBRARY=$(BUILD_PATH)$(LIB_TARGET).a
EXECUTABLE_TEST=$(BUILD_PATH)$(TEST_TARGET).out

ifeq ($(DEBUG),1)
   CFLALGS = -g $(CFLAGS)
endif

all: $(EXECUTABLE) $(LIBRARY)

run: $(EXECUTABLE) $(LIBRARY)
	./$<

debug: $(EXECUTABLE) $(LIBRARY)
	./$<

test: $(EXECUTABLE_TEST) $(LIBRARY)
	./$<

clean :
	rm -f $(OBJ) $(LIB_OBJ) $(EXECUTABLE) $(LIBRARY) $(EXECUTABLE_TEST)

$(EXECUTABLE) : $(OBJ) $(LIBRARY)
	$(CC) $(CFLAGS) $(OBJ) $(LIBRARY) -o $@ $(SFML_FLAGS)

$(OBJ_PATH)$(TARGET)/%.o : $(SOURCES_PATH)%.cpp
	$(CC) $(CFLAGS) $(ALL_HEADERS_INCLUDE) -c $< -o $@

$(LIBRARY) : $(LIB_OBJ)
	ar rcs $@ $^

$(OBJ_PATH)$(LIB_TARGET)/%.o : $(LIB_SOURCES_PATH)%.cpp
	$(CC) $(CFLAGS) $(ALL_HEADERS_INCLUDE) -c $< -o $@ 