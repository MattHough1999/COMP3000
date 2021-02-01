# COMP3016 Matthew Hough ID: 10616194

## Project Details
The project was created using Visual Studio Community Edition version 16.8.1 (Most recent to date) On Windows 10 Education Edition

## Running The Project
This Project runs without error (on my machine) and the executable should not require any additional requirements
The solution requires libraries that need to be built from source which requires adding files to Operating system directories.
The libraries I have used are:
-freeglut
-glew
Tutorials for their instalation can be found at: "https://sites.google.com/site/gsucomputergraphics/educational/set-up-opengl#TOC-How-to-build-freeglut-"

There are no other custom requirements to allow it to run properly. 

## How does it work
### 	Init
	-Init method enables depth test to allow us to render 3d objects in space. It also removes visible cursor from the screen and centers it.
### 	Main
	-Sets our display mode, window height/width and gives the window's title.
	-initialises glut functions that allow the glut loop to run.
###	drawQuad
	-Takes values passed to it and draws a quad.
	-Requires a texture, the texture data, size and position of the 4 vertices.
###	drawWalls
	-draws 3 beige walls.
	-draws 1 red wall.	
###	drawFloorCieling
	-draws scene floor and cieling.
	-applies two different textures.
###	drawTable
	-draws a collection of primitives to resemble a table.
###	display
	-calls camera and draw functions as part of the main loop.
###	

## Navigating my code
I believe my code is properly indented so following it shouldn't be an issue
Some methods are a little messy or inefficient but their purpose will be covered in the video

## Fitting in
My code fits together very simply
each method is seperate and most are isolated
In other words, there are only a few methods that call other methods and the others should all function independently
The "InspectForFirst" methods contain the same code with an additional line added to run the primary inspection before finding the subnode and or attribute.

## YouTube Link
https://youtu.be/hyvM6AFUU3k
