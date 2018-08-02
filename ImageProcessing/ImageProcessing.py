import numpy as np 
import os
import sys

from scipy import ndimage as ndi
import scipy
from skimage.color import rgb2gray
from skimage import feature
from skimage.filters import roberts, sobel, scharr, prewitt
from skimage.util import random_noise

rootDir = "D:\\Temp\\Upload\\"
images = []

for root, dirs, files in os.walk(rootDir + "Albums"):  
    for filename in files:
        if  '.csv' not in filename and 'Uploaded' not in root:
            imagePath = root + "\\" + filename #os.path.join(root, filename)
            images.append(imagePath)    
      

for image in images:    
    try:
        im = scipy.misc.imread(image)

        img_gray = rgb2gray(im) 

        edge_roberts = roberts(img_gray)
        edge_sobel = sobel(img_gray)
        edge_canny1 = feature.canny(img_gray)
        edge_canny2 = feature.canny(img_gray, sigma=0.2)
        #edge_canny3 = feature.canny(img_gray, sigma=0.25)
        edge_canny4 = feature.canny(img_gray, sigma=0.3)
        edge_prewitt = prewitt(img_gray)

        noise_gaussian = random_noise(im, "gaussian") 
        noise_salt = random_noise(im, "salt") 
        noise_pepper = random_noise(im, "pepper") 
        noise_speckle = random_noise(im, "speckle") 
        
        pathGrayscale = rootDir + "Processed\\Colormode\\Grayscale\\"

        if not os.path.exists(pathGrayscale):
            os.makedirs(pathGrayscale)
    
        pathRoberts = rootDir + "Processed\\Edge\\Roberts\\"

        if not os.path.exists(pathRoberts):
            os.makedirs(pathRoberts)

        pathSobel = rootDir + "Processed\\Edge\\Sobel\\"

        if not os.path.exists(pathSobel):
            os.makedirs(pathSobel)

        pathCanny1 = rootDir + "Processed\\Edge\\Canny\\"

        if not os.path.exists(pathCanny1):
            os.makedirs(pathCanny1)

        pathCanny2 = rootDir + "Processed\\Edge\\Canny0.2\\"

        if not os.path.exists(pathCanny2):
            os.makedirs(pathCanny2)

        pathCanny3 = rootDir + "Processed\\Edge\\Canny0.25\\"

        if not os.path.exists(pathCanny3):
            os.makedirs(pathCanny3)

        pathCanny4 = rootDir + "Processed\\Edge\\Canny0.3\\"

        if not os.path.exists(pathCanny4):
            os.makedirs(pathCanny4)

        pathPrewitt = rootDir + "Processed\\Edge\\Prewitt\\"

        if not os.path.exists(pathPrewitt):
            os.makedirs(pathPrewitt)

        gaussian = rootDir + "Processed\\Noise\\Gaussian\\"

        if not os.path.exists(gaussian):
            os.makedirs(gaussian)

        salt = rootDir + "Processed\\Noise\\Salt\\"

        if not os.path.exists(salt):
            os.makedirs(salt)

        pepper = rootDir + "Processed\\Noise\\Pepper\\"

        if not os.path.exists(pepper):
            os.makedirs(pepper)

        speckle = rootDir + "Processed\\Noise\\Speckle\\"

        if not os.path.exists(speckle):
            os.makedirs(speckle)

        scipy.misc.imsave(pathGrayscale + image.split('\\')[-1] , img_gray)
        scipy.misc.imsave(pathRoberts + image.split('\\')[-1] , edge_roberts)
        scipy.misc.imsave(pathSobel + image.split('\\')[-1] , edge_sobel)
        scipy.misc.imsave(pathCanny1 + image.split('\\')[-1] , edge_canny1)
        scipy.misc.imsave(pathCanny2 + image.split('\\')[-1] , edge_canny2)
        #scipy.misc.imsave(pathCanny3 + image.split('\\')[-1] , edge_canny3)
        scipy.misc.imsave(pathCanny4 + image.split('\\')[-1] , edge_canny4)
        scipy.misc.imsave(pathPrewitt + image.split('\\')[-1] , edge_prewitt)

        scipy.misc.imsave(gaussian + image.split('\\')[-1] , noise_gaussian)
        scipy.misc.imsave(salt + image.split('\\')[-1] , noise_salt)
        scipy.misc.imsave(pepper + image.split('\\')[-1] , noise_pepper)
        scipy.misc.imsave(speckle + image.split('\\')[-1] , noise_speckle)

        print("Image processing completed for :" , image.split('\\')[-1])
    except:
        print("Unexpected error occured processing image :" , image.split('\\')[-1] , " Error : ", sys.exc_info()[0])
    





    