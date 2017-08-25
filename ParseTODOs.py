# Парсит все файлы с расширением .cs в каталоге '.\Assets'
# Все TODO и FIXME сохраняются, потом дописываются в файл
# '.\Assets\_Documentation\TODO.txt' между тегами '*Code:' и '*Code'
import re
import os

def parseFile( path, todos):
    name = re.search('\\\(\w+?)\.cs$', path, re.IGNORECASE)
    if name:
        className = name.group(1)
    else:
        className = 'ERROR!'
    f = open(path)
    for line in f:
        result = re.search('//\s*(TODO|FIXME):\s*(.+)$', line, re.IGNORECASE)
        if result:
            if result.group(1).lower() == 'todo':
                if className not in todos['TODO:']:
                    todos['TODO:'][className] = []
                todos['TODO:'][className].append(result.group(2))
            else:
                if className not in todos['FIXME:']:
                    todos['FIXME:'][className] = []
                todos['FIXME:'][className].append(result.group(2))
	f.close()

def printToDOs( k, todos, fw):
    if todos[k]:
        fw.write('\t'+k+'\n')
        for key in todos[k]:
            fw.write('\t\t'+key+'\n')
            for line in todos[k][key]:
                fw.write('\t\t\t'+line+'\n')

def parseAllFiles(todos):
    for subdir, dirs, files in os.walk(os.getcwd()+'\Assets'):
            for file in files:
                    ext = os.path.splitext(file)[-1].lower()
                    if ext == '.cs':
                        fullpath = os.path.join(subdir,file)
                        parseFile(fullpath, todos)

def writeTODOFile( todos):
    finalPath = os.getcwd()+'\Assets\_Documentation\TODO.txt'
    fr = open(finalPath,'r')
    rd = True
    fl = []
    for line in fr:
        if line.lower() == '*code\n':
            rd = True
        if rd:
            fl.append(line)
        if line.lower() == '*code:\n':
            rd = False
    fr.close()
    fw = open(finalPath, 'w')
    for line in fl:
        fw.write(line)
        if line.lower() == '*code:\n':
            printToDOs('TODO:',todos,fw)
            printToDOs('FIXME:',todos,fw)
    fw.close()

todos = {'TODO:': {}, 'FIXME:': {}}
parseAllFiles(todos)
writeTODOFile( todos)



